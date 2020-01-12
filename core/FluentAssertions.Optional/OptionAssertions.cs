using System;
using AlphaDev.Optional.Extensions.Unsafe;
using FluentAssertions.Equivalency;
using FluentAssertions.Execution;
using FluentAssertions.Optional.Extensions;
using Optional;

namespace FluentAssertions.Optional
{
    public class OptionAssertions<T>
    {
        public OptionAssertions(Option<T> subject) => Subject = subject;

        public Option<T> Subject { get; }

        public AndWhichConstraint<OptionAssertions<T>, T> HaveSome(string because = "", params object[] becauseArgs)
        {
            return Subject.Map(arg => new AndWhichConstraint<OptionAssertions<T>, T>(this, arg))
                          .WithException(() => Execute.Assertion.BecauseOf(because, becauseArgs))
                          .ValueOrFailure(scope => scope.FailWith("Option does not have a value."));
        }

        public void HasValueEquivalentTo<TExpected>(TExpected value, string because = "", params object[] becauseArgs)
        {
            HasValueEquivalentTo(value, options => options, because, becauseArgs);
        }

        public void HasValueEquivalentTo<TExpected>(TExpected value,
            Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config, string because = "",
            params object[] becauseArgs)
        {
            Subject.Match(arg => arg.AssertEquality(value, config, because, becauseArgs),
                () => Execute.Assertion.BecauseOf(because, becauseArgs).FailWith("Option does not have a value."));
        }

        public AndConstraint<OptionAssertions<T>> BeNone(string because = "", params object[] becauseArgs)
        {
            return Subject.WithException(() => new AndConstraint<OptionAssertions<T>>(this))
                          .Map(arg => Execute.Assertion.BecauseOf(because, becauseArgs))
                          .ExceptionOrFailure(scope => scope.FailWith("Option has a value."));
        }
    }
}