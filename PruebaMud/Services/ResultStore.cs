using PruebaMud.Models;

namespace PruebaMud.Services;

public class ResultStore
{
    public PatientAssaysFile? Current { get; private set; }

    public bool HasData => Current is not null;

    public void Set(PatientAssaysFile data)
    {
        Current = data;
    }

    public void Clear()
    {
        Current = null;
    }
}