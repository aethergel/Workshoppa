using System.Collections.Generic;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace Workshoppa.External;

internal sealed class ExternalPluginHandler
{
    private readonly PandoraIpc _pandoraIpc;

    private bool? _pandoraState;

    public ExternalPluginHandler()
    {
        _pandoraIpc = new PandoraIpc();
    }

    public bool Saved { get; private set; }

    public void Save()
    {
        if (Saved)
        {
            Service._pluginLog.Information("Not overwriting external plugin state");
            return;
        }

        Service._pluginLog.Information("Saving external plugin state...");
        SaveYesAlreadyState();
        SavePandoraState();
        Saved = true;
    }

    private void SaveYesAlreadyState()
    {
        if (Service._pluginInterface.TryGetData<HashSet<string>>("YesAlready.StopRequests", out var data) &&
            !data.Contains(nameof(Workshoppa)))
        {
            Service._pluginLog.Debug("Disabling YesAlready");
            data.Add(nameof(Workshoppa));
        }
    }

    private void SavePandoraState()
    {
        _pandoraState = _pandoraIpc.DisableIfNecessary();
        Service._pluginLog.Information($"Previous pandora feature state: {_pandoraState}");
    }

    /// <summary>
    /// Unlike Pandora/YesAlready, we only disable TextAdvance during the item turn-in so that the cutscene skip
    /// still works (if enabled).
    /// </summary>
    public void SaveTextAdvance()
    {
        if (Service._pluginInterface.TryGetData<HashSet<string>>("TextAdvance.StopRequests", out var data) &&
            !data.Contains(nameof(Workshoppa)))
        {
            Service._pluginLog.Debug("Disabling textadvance");
            data.Add(nameof(Workshoppa));
        }
    }

    public void Restore()
    {
        if (Saved)
        {
            RestoreYesAlready();
            RestorePandora();
        }

        Saved = false;
        _pandoraState = null;
    }

    private void RestoreYesAlready()
    {
        if (Service._pluginInterface.TryGetData<HashSet<string>>("YesAlready.StopRequests", out var data) &&
            data.Contains(nameof(Workshoppa)))
        {
            Service._pluginLog.Debug("Restoring YesAlready");
            data.Remove(nameof(Workshoppa));
        }
    }

    private void RestorePandora()
    {
        Service._pluginLog.Information($"Restoring previous pandora state: {_pandoraState}");
        if (_pandoraState == true)
            _pandoraIpc.Enable();
    }

    public void RestoreTextAdvance()
    {
        if (Service._pluginInterface.TryGetData<HashSet<string>>("TextAdvance.StopRequests", out var data) &&
            data.Contains(nameof(Workshoppa)))
        {
            Service._pluginLog.Debug("Restoring textadvance");
            data.Remove(nameof(Workshoppa));
        }
    }
}
