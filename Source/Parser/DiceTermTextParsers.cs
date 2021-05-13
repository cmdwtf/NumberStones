
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Options;

using Superpower;
using Superpower.Parsers;

namespace cmdwtf.NumberStones.Parser
{
	internal static class DiceTermTextParsers
	{
		public const string CoinTypeSide = "C";
		public const string FudgeTypeSide = "F";
		public const string PlanechaseTypeSide = "P";

		private static TextParser<DiceTypes.DiceType> DiceKind { get; } =
			from type in Parse.OneOf(
					Span.EqualToIgnoreCase(CoinTypeSide).Value(DiceTypes.DiceType.Coin),
					Span.EqualToIgnoreCase(FudgeTypeSide).Value(DiceTypes.DiceType.Fudge),
					Span.EqualToIgnoreCase(PlanechaseTypeSide).Value(DiceTypes.DiceType.Planar)
				).OptionalOrDefault(DiceTypes.DiceType.Numerical)
			select type;

		private static TextParser<decimal> DiceSides { get; } =
			from sides in Numerics.DecimalDecimal
			select sides;

		private static TextParser<CriticalTypeMode> CriticalType { get; } =
			from mode in Parse.OneOf(
				Character.EqualToIgnoreCase(Critical.SymbolSuccess)
				.Value(CriticalTypeMode.Success),
				Character.EqualToIgnoreCase(Critical.SymbolFailure)
				.Value(CriticalTypeMode.Failure))
			select mode;

		private static TextParser<HighLowMode> HighLowType { get; } =
			from mode in Parse.OneOf(
				Character.EqualToIgnoreCase(Keep.SymbolHigh)
				.Value(HighLowMode.High),
				Character.EqualToIgnoreCase(Keep.SymbolLow)
				.Value(HighLowMode.Low))
			select mode;

		private static TextParser<ExplodingDiceMode> ExplodingType { get; } =
			from mode in Parse.OneOf(
				Character.EqualToIgnoreCase(Exploding.SymbolPenetrating)
				.Try()
				.Value(ExplodingDiceMode.Penetrating),
				Character.EqualToIgnoreCase(Exploding.SymbolCompound)
				.Try()
				.Value(ExplodingDiceMode.Compound)
				).OptionalOrDefault(ExplodingDiceMode.Classic)
			select mode;


		private static TextParser<ComparisonDiceMode> ComparisonMode { get; } =
			from mode in Parse.OneOf(
					Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolEqual)
					.Try()
					.Value(ComparisonDiceMode.Equals),
					Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolGreater)
					.Try()
					.IgnoreThen(
						Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolEqual)
						.Try()
						.Value(ComparisonDiceMode.GreaterThanEquals)
						.OptionalOrDefault(ComparisonDiceMode.GreaterThan)
					),
					Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolLess)
					.Try()
					.IgnoreThen(
						Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolEqual)
						.Try()
						.Value(ComparisonDiceMode.LessThanEquals)
						.OptionalOrDefault(ComparisonDiceMode.LessThan)
					),
					Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolNot)
					.Try()
					.IgnoreThen(
						Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolEqual)
						.Try()
						.Value(ComparisonDiceMode.Not)
					)
				)
			select mode;

		private static TextParser<IDiceOption> CriticalOption { get; } =
			from key in Character.EqualToIgnoreCase(Critical.Symbol)
			from type in CriticalType
			from mode in ComparisonMode
			from val in Numerics.DecimalDecimal
			select new Critical(val, type, mode) as IDiceOption;

		private static TextParser<IDiceOption> DropOption { get; } =
			from key in Character.EqualToIgnoreCase(Drop.Symbol)
			from mode in HighLowType.Try()
				.OptionalOrDefault(HighLowMode.Low)
			from val in Numerics.DecimalDecimal.OptionalOrDefault(1m)
			select new Drop(val, mode) as IDiceOption;

		private static TextParser<IDiceOption> ExplodingOption { get; } =
			from key in Character.EqualToIgnoreCase(Exploding.Symbol)
			from type in ExplodingType
			from mode in ComparisonMode.OptionalOrDefault(ComparisonDiceMode.GreaterThanEquals)
			from val in Numerics.DecimalDecimal.OptionalOrDefault(0m)
			select new Exploding(val, type, mode) as IDiceOption;

		private static TextParser<IDiceOption> KeepOption { get; } =
			from key in Character.EqualToIgnoreCase(Keep.Symbol)
			from mode in HighLowType.Try()
				.OptionalOrDefault(HighLowMode.High)
			from val in Numerics.DecimalDecimal.OptionalOrDefault(1m)
			select new Keep(val, mode) as IDiceOption;

		private static TextParser<IDiceOption> LabelOption { get; } =
			from open in Character.EqualToIgnoreCase(Label.SymbolOpen)
			from text in Character.Except(Label.SymbolClose).Many()
			from close in Character.EqualToIgnoreCase(Label.SymbolClose)
			select new Label(new string(text)) as IDiceOption;

		private static TextParser<IDiceOption> RerollOption { get; } =
			from key in Character.EqualToIgnoreCase(Reroll.Symbol)
			from val in Numerics.DecimalDecimal
			select new Reroll(val) as IDiceOption;

		private static TextParser<IDiceOption> TargetOption { get; } =
			from mode in ComparisonMode
			from val in Numerics.DecimalDecimal
			select new Target(val, mode) as IDiceOption;

		private static TextParser<IDiceOption> TwiceOption { get; } =
			from key in Character.EqualToIgnoreCase(Twice.Symbol)
			from val in Numerics.DecimalDecimal
			select new Twice(val) as IDiceOption;

		internal static TextParser<IDiceOption[]> DiceOptions { get; } =
			from options in Parse.OneOf(
				CriticalOption,
				DropOption,
				ExplodingOption,
				KeepOption,
				LabelOption,
				RerollOption,
				TargetOption,
				TwiceOption
			).Many()
			select options;

		internal static TextParser<DiceSettings> DiceSettingsFull { get; } =
			from multiplicity in Span.MatchedBy(Numerics.Decimal)
				.Apply(Numerics.DecimalDecimal)
				.OptionalOrDefault(1m)
			from seperator in DiceExpressionTextParsers.DiceSeperatorCharacter
			from sides in DiceSides.OptionalOrDefault(0m)
			from kind in DiceKind.Try()
				.Where(k => sides == 0m && k != DiceTypes.DiceType.Numerical)
				.OptionalOrDefault(DiceTypes.DiceType.Numerical)
			from options in DiceOptions
			select new DiceSettings(sides, multiplicity, kind)
			{
				ParsedOptions = options
			};

		internal static TextParser<DiceTerm> DiceTerm { get; } =
			from settings in DiceSettingsFull
			select new DiceTerm(settings);
	}
}
