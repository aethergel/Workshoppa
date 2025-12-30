using Dalamud.IoC;
using Dalamud.Plugin.Services;
using Dalamud.Plugin;

namespace Workshoppa
{
    internal class Service
    {
        [PluginService] internal static IDalamudPluginInterface _pluginInterface { get; private set; } = null!;
        [PluginService] internal static IGameGui _gameGui { get; private set; } = null!;
        [PluginService] internal static IFramework _framework { get; private set; } = null!;
        [PluginService] internal static ICondition _condition { get; private set; } = null!;
        [PluginService] internal static IClientState _clientState { get; private set; } = null!;
        [PluginService] internal static IDataManager _dataManager { get; private set; } = null!;
        [PluginService] internal static IObjectTable _objectTable { get; private set; } = null!;
        [PluginService] internal static ICommandManager _commandManager { get; private set; } = null!;
        [PluginService] internal static IPluginLog _pluginLog { get; private set; } = null!;
        [PluginService] internal static IAddonLifecycle _addonLifecycle { get; private set; } = null!;
        [PluginService] internal static IChatGui _chatGui { get; private set; } = null!;
        [PluginService] internal static ITextureProvider _textureProvider { get; private set; } = null!;
    }
}
