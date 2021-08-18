How to generate Edm objects

1) Make sure to install NuGet

2)Open Nuget Package Manager Console.

2)In the console, set 'Default project' to "AdminService".

3)Run the follow command in the console (assumes your repo is at D:\Repos\, update as necessary): 
	Scaffold-DbContext "Server=localhost;Database=STDemoAdmin;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir D:\Learn\Github\Repos\STDemo\STDemoAdminService\AdminService\Database\STDemoAdmin -Force

4) In STDemoAdminContext.cs:
	a) Remove the 'if' block with connection string information in method 'OnConfiguring(DbContextOptionsBuilder optionsBuilder)'
	b) Remove the default contructor (the constructor with no parameters), we want to force users to pass in options. 



!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

!!!DO NOT INJECT THE DBCONTEXT AT STARTUP: DBContext is not thread safe and should be initialized at the individual thread and disposed of after use.!!!
Example of usage:
	var options = new DbContextOptionsBuilder<EsignatureDataContext>().UseSqlServer(_configuration.GetConnectionString("EsignatureDataContext")).Options;
	using (var dbContext = new EsignatureDataContext(options))
	{
		...
		Your code
		...
	}

!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!