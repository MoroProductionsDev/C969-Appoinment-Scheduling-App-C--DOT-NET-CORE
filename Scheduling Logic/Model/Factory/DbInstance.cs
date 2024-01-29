using Scheduling_Logic.Model.Config;
using Scheduling_Logic.Model.Database;

namespace Scheduling_Logic.Model.Factory
{
    /*
     * Description: This static class applies the S.O.L.I.[D] specially the "D" for "Dependency Inversion".
     *              It will return instances related to data use for the database objects instatiations.
     *              
     *              S.O.L.I.D. stands for: 
     *              S – Single-Responsibility Principle. 
     *              O – Open-Closed Principle. (Open for extension but close to modifications)
     *              L – Liskov Substitution Principle. 
     *              I – Interface Segregation Principle. 
     *              D – Dependency Inversion
     */

    /* References:
     * How to use switch statements on system's type.
     * https://stackoverflow.com/questions/43080505/how-to-switch-on-system-type
     */
    public static class DbInstance
    {
        /*
         * Description: This function creates and returns a new [IDatabaseConnector] object base on the type passed.
         * 
         * @param       [Type] connectionType       It carries a IDbConnection (Type) that will be use to create the
         *                                          right object type.
         *              [String] connectionString   A connection string configuration for the specific
         *                                          database type.
         *                                      
         * @return      A instance of a [IDatabaseConnector].
         */
        public static DbConnector? CreateDatabaseConnector(IDbConfig? config)
        {
            DbConnector? dbConnector = null;
            switch (config?.GetType())
            {
                case Type _ when typeof(MySqlConfig) == config.GetType():
                    {
                        dbConnector = new DbConnector(config);
                        break;
                    }
            }

            return dbConnector;
        }
    }
}
