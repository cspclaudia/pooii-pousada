﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pousada.Data;

namespace Pousada.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("Pousada.Models.Conta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FormaPagamento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ReservaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("ValorTotal")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("ReservaId");

                    b.ToTable("Conta");
                });

            modelBuilder.Entity("Pousada.Models.Hospede", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RG")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Hospede");
                });

            modelBuilder.Entity("Pousada.Models.Quarto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Disponilidade")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Numero")
                        .HasColumnType("INTEGER");

                    b.Property<float>("ValorDiaria")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.ToTable("Quarto");
                });

            modelBuilder.Entity("Pousada.Models.RelatorioDiario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Alimentacao")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ContaId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Data")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Telefonema")
                        .HasColumnType("INTEGER");

                    b.Property<float>("ValorAlimentacao")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<float>("ValorTelefonema")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<float>("ValorTotal")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("ContaId");

                    b.ToTable("RelatorioDiario");
                });

            modelBuilder.Entity("Pousada.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataSaida")
                        .HasColumnType("TEXT");

                    b.Property<int?>("HospedeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("QuartoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HospedeId");

                    b.HasIndex("QuartoId");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("Pousada.Models.Conta", b =>
                {
                    b.HasOne("Pousada.Models.Reserva", "Reserva")
                        .WithMany()
                        .HasForeignKey("ReservaId");
                });

            modelBuilder.Entity("Pousada.Models.RelatorioDiario", b =>
                {
                    b.HasOne("Pousada.Models.Conta", "Conta")
                        .WithMany()
                        .HasForeignKey("ContaId");
                });

            modelBuilder.Entity("Pousada.Models.Reserva", b =>
                {
                    b.HasOne("Pousada.Models.Hospede", "Hospede")
                        .WithMany()
                        .HasForeignKey("HospedeId");

                    b.HasOne("Pousada.Models.Quarto", "Quarto")
                        .WithMany()
                        .HasForeignKey("QuartoId");
                });
#pragma warning restore 612, 618
        }
    }
}
