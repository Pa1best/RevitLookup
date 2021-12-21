using Autodesk.Revit.DB;
using RevitLookup.Views;
using Form = System.Windows.Forms.Form;

namespace RevitLookup.Core.RevitTypes;

public class ElementPhaseStatuses : Data
{
    private readonly Element _element;

    public ElementPhaseStatuses(string label, Element element) : base(label)
    {
        _element = element;
    }

    public override bool HasDrillDown => !_element.Document.Phases.IsEmpty;

    public override string AsValueString()
    {
        return "< phases statuses >";
    }

    public override Form DrillDown()
    {
        if (!HasDrillDown) return null;

        var elementOnPhaseStatuses = _element
            .Document
            .Phases
            .Cast<Phase>()
            .Select(x => new SnoopableObjectWrapper(x.Name, _element.GetPhaseStatus(x.Id)))
            .ToList();

        if (elementOnPhaseStatuses.Count == 0) return null;
        var form = new Objects(elementOnPhaseStatuses);
        return form;
    }
}