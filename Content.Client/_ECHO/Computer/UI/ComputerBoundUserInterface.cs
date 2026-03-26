using Content.Client.CartridgeLoader;
using Content.Shared.CartridgeLoader;
using Robust.Client.UserInterface;

namespace Content.Client._ECHO.Computer.UI;

public sealed partial class ComputerBoundUserInterface : CartridgeLoaderBoundUserInterface
{
    private ComputerWindow? _menu;

    public ComputerBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();

        _menu = this.CreateWindow<ComputerWindow>();

        _menu.SetOpenedWindow(null, null);

        _menu.OnProgramItemPressed += ActivateCartridge;
        _menu.OnInstallButtonPressed += InstallCartridge;
        _menu.OnUninstallButtonPressed += UninstallCartridge;
        _menu.OnCloseItemPressed += DeactivateActiveCartridge;

        _menu.OpenCentered();
    }

    protected override void AttachCartridgeUI(Control cartridgeUIFragment, string? title)
    {
        _menu?.SetOpenedWindow(cartridgeUIFragment, title);
    }

    protected override void DetachCartridgeUI(Control cartridgeUIFragment)
    {
        if (_menu is null)
            return;

        _menu.SetOpenedWindow(null, null);
    }

    protected override void UpdateAvailablePrograms(List<(EntityUid, CartridgeComponent)> programs)
    {
        _menu?.UpdateAvailablePrograms(programs);
    }
}
