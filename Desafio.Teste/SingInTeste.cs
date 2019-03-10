using Domain.Authorize;
using Domain.Models.Entities;
using Domain.SecurityHash;
using Domain.TokenGenerator;
using Infra.Repositories;
using Infra.RepositoriesADO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;


namespace Desafio.Teste
{
    [TestClass]
    public class SingInTeste : Controller
    {

        private readonly UsuarioRepository _usuarioRepository;
        private readonly UsuarioRepositoryADO _usuarioRepositoryADO;
        public SingInTeste(UsuarioRepository usuarioRepository, UsuarioRepositoryADO usuarioRepositoryADO)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioRepositoryADO = usuarioRepositoryADO;
        }

      


         [TestMethod]
        [TestCategory("Usuario")]
        public void TestMethod1()
        {
     
            

                Assert.AreEqual("", "");
            }
            
               
        
    }
}
