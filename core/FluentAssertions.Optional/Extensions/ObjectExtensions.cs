using System;
using FluentAssertions.Equivalency;

namespace FluentAssertions.Optional.Extensions
{
    public static class ObjectExtensions
    {
        public static void AssertEquality<TExpected, TValue>(this object? value, TExpected expected,
            Func<EquivalencyAssertionOptions<TValue>, EquivalencyAssertionOptions<TValue>> config,
            string? because = null,
            params object[] becauseArgs)
        {
            var assertionOptions = config(AssertionOptions.CloneDefaults<TValue>()).AsCollection();
            var context = new EquivalencyValidationContext
            {
                Subject = value,
                Expectation = expected,
                RootIsCollection = true,
                CompileTimeType = typeof(TExpected),
                Because = because,
                BecauseArgs = becauseArgs,
                Tracer = assertionOptions.TraceWriter
            };

            new EquivalencyValidator(assertionOptions).AssertEquality(context);
        }
    }
}