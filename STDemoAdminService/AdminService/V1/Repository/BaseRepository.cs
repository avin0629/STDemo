using AdminService.Database.STDemoAdmin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.V1.Repository
{
	/// <summary>
	/// BaseRepository
	/// </summary>
	public class BaseRepository
	{
		#region Members
		protected readonly ILogger _logger;
		protected readonly IConfiguration _configuration;
		protected readonly string _stAdminDemoConnectionName;
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseRepository"/> class.
		/// </summary>
		/// <param name="configuration"></param>
		public BaseRepository(IConfiguration configuration)
		{
			this._configuration = configuration;
			this._logger = LogManager.GetCurrentClassLogger();
			this._stAdminDemoConnectionName = "STAdminDemo";
		}

		#endregion

		#region Properties
		#endregion

		#region Protected Methods

		/// <summary>
		/// EsignatureData database context.
		/// </summary>
		/// <returns></returns>
		protected STDemoAdminContext GetSTDemoAdminContext()
		{
			var options = new DbContextOptionsBuilder<STDemoAdminContext>()
							.UseSqlServer(_configuration.GetConnectionString(_stAdminDemoConnectionName))
							.Options;

			return new STDemoAdminContext(options);
		}

		#endregion
	}
}
