using CF.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    public class ContextHandler : CreateDatabaseIfNotExists<CfDb>
    {

        protected override void Seed(CfDb context)
        {
            base.Seed(context);
            CreateDefaultData(context);
        }

        public override void InitializeDatabase(CfDb context)
        {
            try
            {
                if (context.Database.Exists()) return;
                base.InitializeDatabase(context);
            }
            catch (SqlException ex) { throw new ArgumentException(ex.Message, ex); }
        }

        private void CreateDefaultData(CfDb context)
        {
            
        }

        private static bool FindError(SqlErrorCollection errors, string error)
        {
            return errors.Cast<SqlError>().ToList().Exists(x => x.ToString().ToLower().Contains(error));
        }
    }
}
