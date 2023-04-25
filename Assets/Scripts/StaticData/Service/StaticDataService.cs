using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Infrastructure.AssetManagement;
using Scripts.UI.Screens;
using Scripts.UI.Services.Screens;

namespace Scripts.StaticData.Service
{
    public class StaticDataService : IStaticDataService
    {
        private const string ScreensPath = "Screens";
        private const string HeroPath = "HeroDefaultData";
        private const string EnemiesPath = "EnemiesData";

        private readonly IAssets _assets;

        private Dictionary<ScreenId, BaseScreen> _screens;
        private HeroDefaultStaticData _hero;
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;

        public StaticDataService(IAssets assets)
        {
            _assets = assets;
        }

        public async Task Load()
        {
            await Task.WhenAll(
                LoadScreens(),
                LoadHeroDefaultData(),
                LoadEnemiesData());
        }

        public BaseScreen GetScreen(ScreenId screenId) =>
            _screens.TryGetValue(screenId, out BaseScreen screen)
                ? screen
                : null;

        public HeroDefaultStaticData GetHero() => _hero;

        public EnemyStaticData GetEnemy(EnemyTypeId enemyTypeId) =>
            _enemies.TryGetValue(enemyTypeId, out EnemyStaticData enemy) 
                ? enemy 
                : null;

        private async Task LoadScreens()
        {
            ScreenStaticData screensData = await _assets.Load<ScreenStaticData>(ScreensPath);
            _screens = new Dictionary<ScreenId, BaseScreen>(screensData.Screens);
        }

        private async Task LoadHeroDefaultData() => 
            _hero = await _assets.Load<HeroDefaultStaticData>(HeroPath);
        
        private async Task LoadEnemiesData()
        {
            IEnumerable<EnemyStaticData> loaded = await _assets.LoadAll<EnemyStaticData>(EnemiesPath);
            _enemies = loaded.ToDictionary(x => x.EnemyType, x => x);
        }

    }
}