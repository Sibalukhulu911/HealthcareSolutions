namespace HealthcareSolutions.Client.Services;

public static class RoleHelper
{
    public const string Admin = "Admin";
    public const string Receptionist = "Receptionist";
    public const string Practitioner = "Practitioner";
    public const string Pharmacist = "Pharmacist";

    public static bool IsAdmin(string? role)
    {
        return role == Admin;
    }

    public static bool IsReceptionist(string? role)
    {
        return role == Receptionist;
    }

    public static bool IsPractitioner(string? role)
    {
        return role == Practitioner;
    }

    public static bool IsPharmacist(string? role)
    {
        return role == Pharmacist;
    }
}