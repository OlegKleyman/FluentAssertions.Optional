using System;
using AlphaDev.Optional.Extensions.Unsafe;
using FluentAssertions.Equivalency;
using FluentAssertions.Execution;
using FluentAssertions.Optional.Extensions;
using Optional;

namespace FluentAssertions.Optional
{
    public class OptionAssertions<T, TException>
    {
        public OptionAssertions(Option<T, TException> subject) => Subject = subject;

        public Option<T, TException> Subject { get; }

        public AndWhichConstraint<OptionAssertions<T, TException>, T> HaveSome(string because = "",
            params object[] becauseArgs)
        {
            return Subject.Map(arg => new AndWhichConstraint<OptionAssertions<T, TException>, T>(this, arg))
                          .MapException(exception => Execute.Assertion.BecauseOf(because, becauseArgs))
                          .ValueOrFailure(exception => exception.FailWith("Option does not have a value."));
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
                _ => Execute.Assertion.BecauseOf(because, becauseArgs).FailWith("Option does not have a value."));
        }

        public void HasExceptionEquivalentTo<TExpected>(TExpected value, string because = "",
            params object[] becauseArgs)
        {
            HasExceptionEquivalentTo(value, options => options, because, becauseArgs);
        }

        public void HasExceptionEquivalentTo<TExpected>(TExpected value,
            Func<EquivalencyAssertionOptions<TException>, EquivalencyAssertionOptions<TException>> config,
            string because = "",
            params object[] becauseArgs)
        {
            Subject.Match(arg => Execute.Assertion.BecauseOf(because, becauseArgs).FailWith("Option has a value."),
                exception => exception.AssertEquality(value, config, because, becauseArgs));
        }

        public AndWhichConstraint<OptionAssertions<T, TException>, TException> BeNone(string because = "",
            params object[] becauseArgs)
        {
            return Subject.MapException(arg =>
                              new AndWhichConstraint<OptionAssertions<T, TException>, TException>(this, arg))
                          .Map(arg => Execute.Assertion.BecauseOf(because, becauseArgs))
                          .ExceptionOrFailure(scope => scope.FailWith("Option has a value."));
        }
    }
}