using BrawlhallaStat.Domain.Games;
using BrawlhallaStat.Domain.Statistics;

namespace BrawlhallaStat.Domain.Tests.Statistics;

public class StatisticGeneralFilterTests
{
    [Test]
    public void GIVEN_Filter_WHEN_Legend_and_weapon_not_null_THEN_not_valid_expected()
    {
        var filter = new StatisticGeneralFilter
        {
            LegendId = 1,
            WeaponId = 2,
        };

        Assert.That(filter.IsValid(), Is.EqualTo(false));
    }

    [Test]
    public void GIVEN_Filter_WHEN_Enemy_legend_and_weapon_not_null_THEN_not_valid_expected()
    {
        var filter = new StatisticGeneralFilter
        {
            EnemyLegendId = 11,
            EnemyWeaponId = 22,
        };

        Assert.That(filter.IsValid(), Is.EqualTo(false));
    }

    [TestCase(GameType.Unranked1V1)]
    [TestCase(GameType.Unranked2V2)]
    [TestCase(GameType.Ranked1V1)]
    [TestCase(GameType.Ranked2V2)]
    [TestCase(null)]
    public void GIVEN_Filter_WHEN_Teammate_legend_and_weapon_not_null_and_different_game_types_THEN_not_valid_expected(GameType? gameType)
    {
        var filter = new StatisticGeneralFilter
        {
            GameType = gameType,
            TeammateLegendId = 11,
            TeammateWeaponId = 22,
        };

        Assert.That(filter.IsValid(), Is.EqualTo(false));
    }

    [TestCase(GameType.Unranked1V1, false)]
    [TestCase(GameType.Unranked2V2, true)]
    [TestCase(GameType.Ranked1V1, false)]
    [TestCase(GameType.Ranked2V2, true)]
    public void GIVEN_Filter_WHEN_Teammate_legend_not_null_and_different_game_types_THEN_valid_expected_when_game_type_is_2v2(GameType gameType, bool expected)
    {
        var filter = new StatisticGeneralFilter
        {
            GameType = gameType,
            TeammateLegendId = 11,
        };

        Assert.That(filter.IsValid(), Is.EqualTo(expected));
    }

    [TestCase(GameType.Unranked1V1, false)]
    [TestCase(GameType.Unranked2V2, true)]
    [TestCase(GameType.Ranked1V1, false)]
    [TestCase(GameType.Ranked2V2, true)]
    public void GIVEN_Filter_WHEN_Teammate_weapon_not_null_and_different_game_types_THEN_valid_expected_when_game_type_is_2v2(GameType gameType, bool expected)
    {
        var filter = new StatisticGeneralFilter
        {
            GameType = gameType,
            TeammateWeaponId = 11,
        };

        Assert.That(filter.IsValid(), Is.EqualTo(expected));
    }
}
