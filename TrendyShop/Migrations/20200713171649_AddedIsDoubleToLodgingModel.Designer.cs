﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrendyShop.Data;

namespace TrendyShop.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200713171649_AddedIsDoubleToLodgingModel")]
    partial class AddedIsDoubleToLodgingModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrendyShop.Models.CashRegister", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Cash")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("CashRegisters");
                });

            modelBuilder.Entity("TrendyShop.Models.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastBlockedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastEntrance")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TrendyShop.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("DefaultSalary")
                        .HasColumnType("real");

                    b.Property<DateTime>("LastPayment")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastRestart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PendingPayment")
                        .HasColumnType("real");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalSalary")
                        .HasColumnType("real");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TrendyShop.Models.ExpendableProduct", b =>
                {
                    b.Property<float?>("Cost")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UnitOfMeasurement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cost", "Name");

                    b.ToTable("ExpendableProducts");
                });

            modelBuilder.Entity("TrendyShop.Models.ExpendableProductStorage", b =>
                {
                    b.Property<float?>("Cost")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float?>("LastInsertedQuantity")
                        .HasColumnType("real");

                    b.Property<DateTime>("LastInsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<float?>("TotalQuantity")
                        .HasColumnType("real");

                    b.HasKey("Cost", "Name");

                    b.ToTable("ExpendableProductStorage");
                });

            modelBuilder.Entity("TrendyShop.Models.ExpenseType", b =>
                {
                    b.Property<int>("ExpenseTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseTypeId");

                    b.ToTable("ExpenseTypes");
                });

            modelBuilder.Entity("TrendyShop.Models.FundMoneyOperation", b =>
                {
                    b.Property<int>("FundMoneyOperationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsExtraction")
                        .HasColumnType("bit");

                    b.HasKey("FundMoneyOperationId");

                    b.ToTable("FundMoneyOperations");
                });

            modelBuilder.Entity("TrendyShop.Models.GastronomicProduct", b =>
                {
                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Cost", "Name");

                    b.ToTable("GastronomicProducts");
                });

            modelBuilder.Entity("TrendyShop.Models.HouseExpense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpenseTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseTypeId");

                    b.ToTable("HouseExpenses");
                });

            modelBuilder.Entity("TrendyShop.Models.Incidence", b =>
                {
                    b.Property<int>("IncidenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LodgingDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LodgingRoomId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IncidenceId");

                    b.HasIndex("LodgingRoomId", "LodgingDate");

                    b.ToTable("Incidences");
                });

            modelBuilder.Entity("TrendyShop.Models.Lodging", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<float?>("Consumption")
                        .HasColumnType("real");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float?>("ExtraCharge")
                        .HasColumnType("real");

                    b.Property<DateTime>("FinalDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDouble")
                        .HasColumnType("bit");

                    b.Property<float>("Prepaid")
                        .HasColumnType("real");

                    b.Property<float?>("RentCost")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.HasKey("RoomId", "Date");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Lodgings");
                });

            modelBuilder.Entity("TrendyShop.Models.Maintenance", b =>
                {
                    b.Property<int>("MaintenanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaintenanceId");

                    b.ToTable("Maintenances");
                });

            modelBuilder.Entity("TrendyShop.Models.MoneyOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsExtraction")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("MoneyOperations");
                });

            modelBuilder.Entity("TrendyShop.Models.Package", b =>
                {
                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Cost", "Name");

                    b.ToTable("Package");
                });

            modelBuilder.Entity("TrendyShop.Models.PurchasePerLodging", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("Name", "Cost", "RoomId", "Date");

                    b.HasIndex("Cost", "Name");

                    b.HasIndex("RoomId", "Date");

                    b.ToTable("PurchasePerLodgings");
                });

            modelBuilder.Entity("TrendyShop.Models.Reservation", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Companion")
                        .HasColumnType("int");

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FinalDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("Romantic")
                        .HasColumnType("bit");

                    b.Property<float?>("StandardPrice")
                        .IsRequired()
                        .HasColumnType("real");

                    b.HasKey("RoomId", "Date");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("TrendyShop.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("RoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("TrendyShop.Models.RoomProduct", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("Name", "Cost", "RoomId");

                    b.HasIndex("RoomId");

                    b.HasIndex("Cost", "Name");

                    b.ToTable("RoomProducts");
                });

            modelBuilder.Entity("TrendyShop.Models.StockTaking", b =>
                {
                    b.Property<int>("StockTakingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Fund")
                        .HasColumnType("real");

                    b.HasKey("StockTakingId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("TrendyShop.Models.StorageRow", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<float>("AmountAvailable")
                        .HasColumnType("real");

                    b.Property<float>("AmountInRooms")
                        .HasColumnType("real");

                    b.HasKey("Name", "Cost");

                    b.HasIndex("Cost", "Name");

                    b.ToTable("Storage");
                });

            modelBuilder.Entity("TrendyShop.Models.ExpendableProductStorage", b =>
                {
                    b.HasOne("TrendyShop.Models.ExpendableProduct", "ExpendableProduct")
                        .WithMany()
                        .HasForeignKey("Cost", "Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrendyShop.Models.HouseExpense", b =>
                {
                    b.HasOne("TrendyShop.Models.ExpenseType", "ExpenseType")
                        .WithMany()
                        .HasForeignKey("ExpenseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrendyShop.Models.Incidence", b =>
                {
                    b.HasOne("TrendyShop.Models.Lodging", null)
                        .WithMany("Incidences")
                        .HasForeignKey("LodgingRoomId", "LodgingDate");
                });

            modelBuilder.Entity("TrendyShop.Models.Lodging", b =>
                {
                    b.HasOne("TrendyShop.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("TrendyShop.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("TrendyShop.Models.Package", b =>
                {
                    b.HasOne("TrendyShop.Models.GastronomicProduct", "GastronomicProduct")
                        .WithMany()
                        .HasForeignKey("Cost", "Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrendyShop.Models.PurchasePerLodging", b =>
                {
                    b.HasOne("TrendyShop.Models.GastronomicProduct", "GastronomicProduct")
                        .WithMany()
                        .HasForeignKey("Cost", "Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrendyShop.Models.Lodging", "Lodging")
                        .WithMany()
                        .HasForeignKey("RoomId", "Date")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrendyShop.Models.Reservation", b =>
                {
                    b.HasOne("TrendyShop.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrendyShop.Models.RoomProduct", b =>
                {
                    b.HasOne("TrendyShop.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrendyShop.Models.GastronomicProduct", "GastronomicProduct")
                        .WithMany()
                        .HasForeignKey("Cost", "Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrendyShop.Models.StorageRow", b =>
                {
                    b.HasOne("TrendyShop.Models.GastronomicProduct", "GastronomicProduct")
                        .WithMany()
                        .HasForeignKey("Cost", "Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
