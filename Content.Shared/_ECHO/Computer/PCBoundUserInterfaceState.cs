using Content.Shared.CartridgeLoader;
using Robust.Shared.Serialization;

namespace Content.Shared._ECHO.Computer;

[Serializable, NetSerializable]
public sealed partial class PCBoundUserInterfaceState : CartridgeLoaderUiState
{
    public bool Enabled;

    public PCBoundUserInterfaceState(bool enabled, NetEntity? activeUi, List<NetEntity> programs) : base(programs, activeUi)
    {
        Enabled = enabled;
    }
}

[Serializable, NetSerializable]
public enum PCBoundUiKey : byte
{
    Key
}
