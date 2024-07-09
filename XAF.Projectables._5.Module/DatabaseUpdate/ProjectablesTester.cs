using DevExpress.ExpressApp;
using XAF.Projectables._5.Module.BusinessObjects;

namespace XAF.Projectables._5.Module.DatabaseUpdate;

public static class ProjectablesTester
{
    public static void TestProjectables(IObjectSpace objectSpace)
    {
        try
        {
            // 👇🏾 This should fail - it's a non-Projectable property
            _ = objectSpace.GetObjectsQuery<ApplicationUser>().OrderBy(u => u.RolesCount_NON_Projectable).ToList();
            throw new InvalidOperationException("This should fail (non-Projectable)");
        }
        catch (Exception e)
        {
            Console.WriteLine("Non-Projectable failed, as expected");
        }

        try
        {
            Projectables5EFCoreDbContext.PerformLogging = true;

            // 👇🏾 This should NOT fail - it IS a Projectable property
            _ = objectSpace.GetObjectsQuery<ApplicationUser>().OrderBy(u => u.RolesCountProjectable).ToList();

            Projectables5EFCoreDbContext.PerformLogging = false;
            Console.WriteLine("Projectables worked!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Projectables didn't work: [{e}]");
        }
    }
}