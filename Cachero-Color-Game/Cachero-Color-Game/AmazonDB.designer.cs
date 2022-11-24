﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cachero_Color_Game
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Group2-RoyalCasino")]
	public partial class AmazonDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Inserttable_Customer(table_Customer instance);
    partial void Updatetable_Customer(table_Customer instance);
    partial void Deletetable_Customer(table_Customer instance);
    #endregion
		
		public AmazonDBDataContext() : 
				base(global::Cachero_Color_Game.Properties.Settings.Default.Group2_RoyalCasinoConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public AmazonDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AmazonDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AmazonDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AmazonDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<table_Customer> table_Customers
		{
			get
			{
				return this.GetTable<table_Customer>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.table_Customers")]
	public partial class table_Customer : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Customer_ID;
		
		private string _Customer_Username;
		
		private string _Customer_Password;
		
		private string _Customer_FirstName;
		
		private string _Customer_MiddleName;
		
		private string _Customer_LastName;
		
		private int _ID_ID;
		
		private string _Customer_IDNumber;
		
		private string _Customer_PhoneNumber;
		
		private decimal _Customer_CurrentBalance;
		
		private decimal _Customer_TotalSpent;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCustomer_IDChanging(int value);
    partial void OnCustomer_IDChanged();
    partial void OnCustomer_UsernameChanging(string value);
    partial void OnCustomer_UsernameChanged();
    partial void OnCustomer_PasswordChanging(string value);
    partial void OnCustomer_PasswordChanged();
    partial void OnCustomer_FirstNameChanging(string value);
    partial void OnCustomer_FirstNameChanged();
    partial void OnCustomer_MiddleNameChanging(string value);
    partial void OnCustomer_MiddleNameChanged();
    partial void OnCustomer_LastNameChanging(string value);
    partial void OnCustomer_LastNameChanged();
    partial void OnID_IDChanging(int value);
    partial void OnID_IDChanged();
    partial void OnCustomer_IDNumberChanging(string value);
    partial void OnCustomer_IDNumberChanged();
    partial void OnCustomer_PhoneNumberChanging(string value);
    partial void OnCustomer_PhoneNumberChanged();
    partial void OnCustomer_CurrentBalanceChanging(decimal value);
    partial void OnCustomer_CurrentBalanceChanged();
    partial void OnCustomer_TotalSpentChanging(decimal value);
    partial void OnCustomer_TotalSpentChanged();
    #endregion
		
		public table_Customer()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Customer_ID
		{
			get
			{
				return this._Customer_ID;
			}
			set
			{
				if ((this._Customer_ID != value))
				{
					this.OnCustomer_IDChanging(value);
					this.SendPropertyChanging();
					this._Customer_ID = value;
					this.SendPropertyChanged("Customer_ID");
					this.OnCustomer_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_Username", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Customer_Username
		{
			get
			{
				return this._Customer_Username;
			}
			set
			{
				if ((this._Customer_Username != value))
				{
					this.OnCustomer_UsernameChanging(value);
					this.SendPropertyChanging();
					this._Customer_Username = value;
					this.SendPropertyChanged("Customer_Username");
					this.OnCustomer_UsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_Password", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Customer_Password
		{
			get
			{
				return this._Customer_Password;
			}
			set
			{
				if ((this._Customer_Password != value))
				{
					this.OnCustomer_PasswordChanging(value);
					this.SendPropertyChanging();
					this._Customer_Password = value;
					this.SendPropertyChanged("Customer_Password");
					this.OnCustomer_PasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_FirstName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Customer_FirstName
		{
			get
			{
				return this._Customer_FirstName;
			}
			set
			{
				if ((this._Customer_FirstName != value))
				{
					this.OnCustomer_FirstNameChanging(value);
					this.SendPropertyChanging();
					this._Customer_FirstName = value;
					this.SendPropertyChanged("Customer_FirstName");
					this.OnCustomer_FirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_MiddleName", DbType="NVarChar(100)")]
		public string Customer_MiddleName
		{
			get
			{
				return this._Customer_MiddleName;
			}
			set
			{
				if ((this._Customer_MiddleName != value))
				{
					this.OnCustomer_MiddleNameChanging(value);
					this.SendPropertyChanging();
					this._Customer_MiddleName = value;
					this.SendPropertyChanged("Customer_MiddleName");
					this.OnCustomer_MiddleNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_LastName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Customer_LastName
		{
			get
			{
				return this._Customer_LastName;
			}
			set
			{
				if ((this._Customer_LastName != value))
				{
					this.OnCustomer_LastNameChanging(value);
					this.SendPropertyChanging();
					this._Customer_LastName = value;
					this.SendPropertyChanged("Customer_LastName");
					this.OnCustomer_LastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_ID", DbType="Int NOT NULL")]
		public int ID_ID
		{
			get
			{
				return this._ID_ID;
			}
			set
			{
				if ((this._ID_ID != value))
				{
					this.OnID_IDChanging(value);
					this.SendPropertyChanging();
					this._ID_ID = value;
					this.SendPropertyChanged("ID_ID");
					this.OnID_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_IDNumber", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Customer_IDNumber
		{
			get
			{
				return this._Customer_IDNumber;
			}
			set
			{
				if ((this._Customer_IDNumber != value))
				{
					this.OnCustomer_IDNumberChanging(value);
					this.SendPropertyChanging();
					this._Customer_IDNumber = value;
					this.SendPropertyChanged("Customer_IDNumber");
					this.OnCustomer_IDNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_PhoneNumber", DbType="VarChar(11) NOT NULL", CanBeNull=false)]
		public string Customer_PhoneNumber
		{
			get
			{
				return this._Customer_PhoneNumber;
			}
			set
			{
				if ((this._Customer_PhoneNumber != value))
				{
					this.OnCustomer_PhoneNumberChanging(value);
					this.SendPropertyChanging();
					this._Customer_PhoneNumber = value;
					this.SendPropertyChanged("Customer_PhoneNumber");
					this.OnCustomer_PhoneNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_CurrentBalance", DbType="Decimal(19,4) NOT NULL")]
		public decimal Customer_CurrentBalance
		{
			get
			{
				return this._Customer_CurrentBalance;
			}
			set
			{
				if ((this._Customer_CurrentBalance != value))
				{
					this.OnCustomer_CurrentBalanceChanging(value);
					this.SendPropertyChanging();
					this._Customer_CurrentBalance = value;
					this.SendPropertyChanged("Customer_CurrentBalance");
					this.OnCustomer_CurrentBalanceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer_TotalSpent", DbType="Decimal(19,4) NOT NULL")]
		public decimal Customer_TotalSpent
		{
			get
			{
				return this._Customer_TotalSpent;
			}
			set
			{
				if ((this._Customer_TotalSpent != value))
				{
					this.OnCustomer_TotalSpentChanging(value);
					this.SendPropertyChanging();
					this._Customer_TotalSpent = value;
					this.SendPropertyChanged("Customer_TotalSpent");
					this.OnCustomer_TotalSpentChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
