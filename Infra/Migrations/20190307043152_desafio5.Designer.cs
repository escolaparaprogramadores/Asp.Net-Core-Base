﻿// <auto-generated />
using System;
using Infra.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190307043152_desafio5")]
    partial class desafio5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Entities.Telefone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ddd");

                    b.Property<string>("Numero");

                    b.Property<Guid?>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Telefone");
                });

            modelBuilder.Entity("Domain.Models.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataAtualizacao");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DataUltimoLogin");

                    b.Property<string>("Email");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Domain.Models.Entities.UsuarioTelefone", b =>
                {
                    b.Property<Guid>("UsuarioId");

                    b.Property<Guid>("TelefoneId");

                    b.HasKey("UsuarioId", "TelefoneId");

                    b.HasIndex("TelefoneId");

                    b.ToTable("UsuarioTelefones");
                });

            modelBuilder.Entity("Domain.Models.Entities.Telefone", b =>
                {
                    b.HasOne("Domain.Models.Entities.Usuario")
                        .WithMany("Telefones")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Domain.Models.Entities.UsuarioTelefone", b =>
                {
                    b.HasOne("Domain.Models.Entities.Telefone", "Telefone")
                        .WithMany()
                        .HasForeignKey("TelefoneId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Models.Entities.Usuario", "Usuario")
                        .WithMany("UsuarioTelefones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}