
using cmdwtf.NumberStones.Expression;
using cmdwtf.NumberStones.Options;

using Superpower;
using Superpower.Parsers;

namespace cmdwtf.NumberStones.Parser
{
	internal static class DiceTextParsers
	{
		public const string CoinSide = "C";
		public const string FateSide = "F";
		public const string PlanechaseSide = "P";

		private static TextParser<DiceTypes.DiceType> DiceType { get; } =
			from type in Parse.OneOf(
					Span.EqualToIgnoreCase(CoinSide).Value(DiceTypes.DiceType.Coin),
					Span.EqualToIgnoreCase(FateSide).Value(DiceTypes.DiceType.Fate),
					Span.EqualToIgnoreCase(PlanechaseSide).Value(DiceTypes.DiceType.Planechase)
				).OptionalOrDefault(DiceTypes.DiceType.Polyhedron)
			select type;

		private static TextParser<decimal> DiceSides { get; } =
			from sides in Numerics.DecimalDecimal
			select sides;

		private static TextParser<CriticalTypeMode> CriticalType { get; } =
			from mode in Character.EqualToIgnoreCase(Critical.SymbolSuccess)
				.Value(CriticalTypeMode.Success)
				.Or(Character.EqualToIgnoreCase(Critical.SymbolFailure)
					.Value(CriticalTypeMode.Failure)
				)
			select mode;

		private static TextParser<HighLowMode> HighOrLowMode { get; } =
			from mode in Parse.OneOf(
				Character.EqualToIgnoreCase(Keep.SymbolHigh)
				.Value(HighLowMode.High),
				Character.EqualToIgnoreCase(Keep.SymbolLow)
				.Value(HighLowMode.Low))
			select mode;

		private static TextParser<ExplodingDiceMode> ExplodingType { get; } =
			from mode in Character.EqualToIgnoreCase(Exploding.SymbolPenetrating)
				.Value(ExplodingDiceMode.Penetrating)
				.Or(Character.EqualToIgnoreCase(Exploding.SymbolCompound)
					.Value(ExplodingDiceMode.Compound)
				).OptionalOrDefault(ExplodingDiceMode.Classic)
			select mode;


		private static TextParser<ComparisonDiceMode> ComparisonMode { get; } =
			from mode in Parse.OneOf(
					Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolEqual)
					.Value(ComparisonDiceMode.Equals),
					Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolGreater)
					.IgnoreThen(
						Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolEqual)
						.Try()
						.Value(ComparisonDiceMode.GreaterThanEquals)
					).OptionalOrDefault(ComparisonDiceMode.GreaterThan),
					Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolLess)
					.IgnoreThen(
						Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolEqual)
						.Try()
						.Value(ComparisonDiceMode.LessThanEquals)
					).OptionalOrDefault(ComparisonDiceMode.LessThan),
					Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolNot)
					.IgnoreThen(
						Character.EqualToIgnoreCase(ComparisonOptionBase.SymbolEqual)
						.Value(ComparisonDiceMode.Not)
					)
				).OptionalOrDefault(ComparisonDiceMode.None)
			select mode;

		private static TextParser<IDiceOption> CriticalOption { get; } =
			from key in Character.EqualToIgnoreCase(Critical.Symbol)
			from val in Numerics.DecimalDecimal
			from type in CriticalType
			from mode in ComparisonMode
			select new Critical(val, type, mode) as IDiceOption;

		private static TextParser<IDiceOption> DropOption { get; } =
			from key in Character.EqualToIgnoreCase(Drop.Symbol)
			from mode in HighOrLowMode.Try()
				.OptionalOrDefault(HighLowMode.High)
			from val in Numerics.DecimalDecimal.OptionalOrDefault(1m)
			select new Drop(val, mode) as IDiceOption;

		private static TextParser<IDiceOption> ExplodingOption { get; } =
			from key in Character.EqualToIgnoreCase(Exploding.Symbol)
			from type in ExplodingType
			from val in Numerics.DecimalDecimal
			from mode in ComparisonMode
			select new Exploding(val, type, mode) as IDiceOption;

		private static TextParser<IDiceOption> KeepOption { get; } =
			from key in Character.EqualToIgnoreCase(Keep.Symbol)
			from mode in HighOrLowMode.Try()
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
			from key in Character.EqualToIgnoreCase(Target.SymbolEqual)
			from mode in ComparisonMode
			from val in Numerics.DecimalDecimal
			select new Target(val, mode) as IDiceOption;

		private static TextParser<IDiceOption> TwiceOption { get; } =
			from key in Character.EqualToIgnoreCase(Twice.Symbol)
			from val in Numerics.DecimalDecimal
			select new Twice(val) as IDiceOption;

		private static TextParser<IDiceOption[]> DiceOptions { get; } =
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
			from type in DiceType
			from options in DiceOptions
			select new DiceSettings(sides, multiplicity)
			{
				ParsedDiceOptions = options
			};

		internal static TextParser<DiceTerm> DiceTerm { get; } =
			from settings in DiceSettingsFull
			select new DiceTerm(settings);
	}
}
