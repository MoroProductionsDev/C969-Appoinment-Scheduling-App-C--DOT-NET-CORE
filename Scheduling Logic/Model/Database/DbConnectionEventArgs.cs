﻿using System; using System.Collections.Generic; using System.Data.Common; using System.Linq; using System.Text; using System.Threading.Tasks;  namespace Scheduling_Logic.Model.Database {     internal class DbConnectionEventArgs : EventArgs     {         public DbConnection Connection { get; }         public DbConnectionEventArgs(DbConnection connection)         {             Connection = connection;         }     } } 