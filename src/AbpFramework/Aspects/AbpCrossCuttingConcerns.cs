using AbpFramework.Application.Services;
using JetBrains.Annotations;
using System;
namespace AbpFramework.Aspects
{
    internal static class AbpCrossCuttingConcerns
    {
        public const string Auditing = "AbpAuditing";
        public static bool IsApplied([NotNull] object obj, [NotNull] string concern)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (concern == null)
            {
                throw new ArgumentNullException(nameof(concern));
            }
            return (obj as IAvoidDuplicateCrossCuttingConcerns)?.AppliedCrossCuttingConcerns.Contains(concern) ?? false;
        }
    }
}
