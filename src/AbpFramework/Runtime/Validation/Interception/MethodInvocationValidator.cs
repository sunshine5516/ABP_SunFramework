using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using AbpFramework.Reflection;
using Castle.Core.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Validation.Interception
{
    public class MethodInvocationValidator : ITransientDependency
    {
        #region 声明实例
        private const int MaxRecursiveParameterValidationDepth = 8;
        protected MethodInfo Method { get; private set; }
        protected object[] ParameterValues { get; private set; }
        protected ParameterInfo[] Parameters { get; private set; }
        protected List<ValidationResult> ValidationErrors { get; }
        private readonly IIocResolver _iocResolver;
        private readonly IValidationConfiguration _configuration;
        #endregion

        #region 构造函数
        public MethodInvocationValidator(IValidationConfiguration configuration, IIocResolver iocResolver)
        {
            _configuration = configuration;
            _iocResolver = iocResolver;
            ValidationErrors = new List<ValidationResult>();
        }
        #endregion
        #region 方法
        /// <summary>
        /// 要验证的方法
        /// </summary>
        /// <param name="method">方法</param>
        /// <param name="parameterValues">用于调用的参数列表</param>
        public virtual void Initialize(MethodInfo method, object[] parameterValues)
        {
            Method = method;
            ParameterValues = parameterValues;
            Parameters = method.GetParameters();
        }
        public void Validate()
        {
            CheckInitialized();
            if (Parameters.IsNullOrEmpty())
            {
                return;
            }
            if (!Method.IsPublic)
            {
                return;
            }
            if (IsValidationDisabled())
            {
                return;
            }
            if (Parameters.Length != ParameterValues.Length)
            {
                throw new Exception("Method parameter count does not match with argument count!");
            }
            if (ValidationErrors.Any() && HasSingleNullArgument())
            {
                throw new Exception("Method arguments are not valid! See ValidationErrors for details!");
            }
            for (var i = 0; i < Parameters.Length; i++)
            {
                ValidateMethodParameter(Parameters[i], ParameterValues[i]);

            }
        }

        #endregion
        #region 辅助方法
        protected virtual void CheckInitialized()
        {
            if (Method == null)
            {
                throw new Exception("对象未初始化.请先调用初始化方法.");
            }
        }
        protected virtual bool IsValidationDisabled()
        {
            if (Method.IsDefined(typeof(EnableValidationAttribute), true))
            {
                return false;
            }
            return ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableValidationAttribute>(Method) != null;
        }
        protected virtual bool HasSingleNullArgument()
        {
            return Parameters.Length == 1 && ParameterValues[0] == null;
        }
        /// <summary>
        /// 验证给定值的给定参数。
        /// </summary>
        /// <param name="parameterInfo">要验证的方法的参数</param>
        /// <param name="parameterValue">要验证的值</param>
        protected virtual void ValidateMethodParameter(ParameterInfo parameterInfo, object parameterValue)
        {
            if (parameterValue == null)
            {
                if (!parameterInfo.IsOptional &&
                    !parameterInfo.IsOut &&
                    !TypeHelper.IsPrimitiveExtendedIncludingNullable(parameterInfo.ParameterType, includeEnums: true))
                {
                    ValidationErrors.Add(new ValidationResult(parameterInfo.Name + " is null!", new[] { parameterInfo.Name }));
                }

                return;
            }
            ValidateObjectRecursively(parameterValue, 1);
        }

        protected virtual void ValidateObjectRecursively(object validatingObject, int currentDepth)
        {
            if (currentDepth > MaxRecursiveParameterValidationDepth)
            {
                return;
            }
            if (validatingObject == null)
            {
                return;
            }
            SetDataAnnotationAttributeErrors(validatingObject);
            //Validate items of enumerable
            if (validatingObject is IEnumerable && !(validatingObject is IQueryable))
            {
                foreach (var item in (validatingObject as IEnumerable))
                {
                    ValidateObjectRecursively(item, currentDepth + 1);
                }
            }
            if (validatingObject is IEnumerable)
            {
                return;
            }
            var validatingObjectType = validatingObject.GetType();
            //不针对原始对象进行递归验证
            if (TypeHelper.IsPrimitiveExtendedIncludingNullable(validatingObjectType))
            {
                return;
            }
            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                if (property.Attributes.OfType<DisableValidationAttribute>().Any())
                {
                    continue;
                }

                ValidateObjectRecursively(property.GetValue(validatingObject), currentDepth + 1);
            }
        }
        /// <summary>
        /// 检查DataAnnotations属性的所有属性。
        /// </summary>
        protected virtual void SetDataAnnotationAttributeErrors(object validatingObject)
        {
            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                var validationAttributes = property.Attributes.OfType<ValidationAttribute>().ToArray();
                if (validationAttributes.IsNullOrEmpty())
                {
                    continue;
                }

                var validationContext = new ValidationContext(validatingObject)
                {
                    DisplayName = property.DisplayName,
                    MemberName = property.Name
                };

                foreach (var attribute in validationAttributes)
                {
                    var result = attribute.GetValidationResult(property.GetValue(validatingObject), validationContext);
                    if (result != null)
                    {
                        ValidationErrors.Add(result);
                    }
                }
            }

            if (validatingObject is IValidatableObject)
            {
                var results = (validatingObject as IValidatableObject).Validate(new ValidationContext(validatingObject));
                ValidationErrors.AddRange(results);
            }
        }
        #endregion
    }
}
