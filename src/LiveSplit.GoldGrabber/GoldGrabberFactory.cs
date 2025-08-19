using System;

using LiveSplit.Model;
using LiveSplit.UI.Components;

[assembly: ComponentFactory(typeof(LiveSplit.GoldGrabber.GoldGrabberFactory))]

namespace LiveSplit.GoldGrabber;

public sealed class GoldGrabberFactory : IComponentFactory
{
    // The name of the component that will be displayed in the layout editor.
    public string ComponentName => GoldGrabberComponent.Name;

    // The tooltip shown when hovering over the component when adding it to the layout.
    public string Description => "A sample autosplitter component for LiveSplit.";

    // Specifies the category of the component.
    // Determines under what category the component is listed when adding it to the layout.
    public ComponentCategory Category => ComponentCategory.Control;

    // A URL to the component's repository.
    public string UpdateURL => "https://github.com/<author>/LiveSplit.GoldGrabber/releases/download";

    // A URL to the component's update XML file.
    public string XMLURL => $"https://raw.githubusercontent.com/absent-friend/LiveSplit.GoldGrabber/main/Components/update.LiveSplit.GoldGrabber.xml";

    // The current version of the component.
    public Version Version => Version.Parse("0.0.1");

    public IComponent Create(LiveSplitState state)
        => new GoldGrabberComponent(state);

    // This property is unused.
    public string UpdateName => throw null;
}
