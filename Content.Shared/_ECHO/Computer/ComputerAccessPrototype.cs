using System.Diagnostics.CodeAnalysis;
using Content.Shared.Dataset;
using Robust.Shared.Prototypes;

namespace Content.Shared._ECHO.Computer;

[Prototype]
public sealed partial class ComputerAccessPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField(required: true)]
    public ProtoId<LocalizedDatasetPrototype>? Names;

    [DataField(required: true)]
    public ProtoId<LocalizedDatasetPrototype> PasswordKeywords;

    [DataField]
    public bool IsGlobal = true;
}
