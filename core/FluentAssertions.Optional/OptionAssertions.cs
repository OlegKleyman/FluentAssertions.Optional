using System;
using System.Collections.Generic;
using FluentAssertions.Equivalency;
using FluentAssertions.Execution;
using FluentAssertions.Optional.Extensions;
using Optional;
using Optional.Unsafe;

namespace FluentAssertions.Optional
{
    public class OptionAssertions<T>
    {
        public OptionAssertions(Option<T> subject) => Subject = subject;

        public Option<T> Subject { get; }

        public AndWhichConstraint<OptionAssertions<T>, T> HaveSome(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion.BecauseOf(because, becauseArgs)
                   .ForCondition(Subject.HasValue)
                   .FailWith("Option does not have a value.");
            return new AndWhichConstraint<OptionAssertions<T>, T>(this, Subject.ValueOrFailure());
        }

        public void HasValueEquivalentTo<TExpected>(TExpected value, string because = "", params object[] becauseArgs)
        {
            HasValueEquivalentTo(value, options => options, because, becauseArgs);
        }

        public void HasValueEquivalentTo<TExpected>(TExpected value, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config, string because = "", params object[] becauseArgs)
        {
            Subject.Match(arg => arg.AssertEquality(value, config, because, becauseArgs),
                () => Execute.Assertion.BecauseOf(because, becauseArgs).FailWith("Option does not have a value."));
        }

        public AndConstraint<OptionAssertions<T>> BeNone(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion.BecauseOf(because, becauseArgs)
                   .ForCondition(!Subject.HasValue)
                   .FailWith("Option has a value.");
            return new AndConstraint<OptionAssertions<T>>(this);
        }
    }
}