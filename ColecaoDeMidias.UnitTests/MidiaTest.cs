using ColecaoDeMidias.Data;
using ColecaoDeMidias.Domain;
using ColecaoDeMidias.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nest;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ColecaoDeMidias.UnitTests
{
    [TestClass]
    public class MidiaTest
    {
        [TestMethod]
        public void DeveriaEmprestarSemInformarMidia()
        {
            var fakeEsClientProvider = new Mock<IESClientProvider>();
            var sut = new MidiaService(fakeEsClientProvider.Object);
        
            var result = sut.Emprestar(0, 1, string.Empty, string.Empty);
            
            NUnit.Framework.Assert.That(result.EhValido, Is.False);
        }

        [TestMethod]
        public void DeveriaCadastrarCdSemTitulo()
        {
            var fakeEsClientProvider = new Mock<IESClientProvider>();
            var sut = new MidiaService(fakeEsClientProvider.Object);

            var result = sut.CadastrarCd(string.Empty, string.Empty, string.Empty, 0);
            
            NUnit.Framework.Assert.That(result.EhValido, Is.False);
        }

        [TestMethod]
        public void DeveriaEmprestarMidiaSemInformarSeuTipo()
        {
            var fakeEsClientProvider = new Mock<IESClientProvider>();
            var sut = new MidiaService(fakeEsClientProvider.Object);
            
            var result = sut.Emprestar(0, 1, "Lucas", "lucas@teste.com");

            NUnit.Framework.Assert.That(result.EhValido, Is.False);
        }

        [TestMethod]
        public void DeveriaDevolverMidiaSemInformarMidiaId()
        {
            var fakeEsClientProvider = new Mock<IESClientProvider>();
            var sut = new MidiaService(fakeEsClientProvider.Object);

            var result = sut.Devolver(TipoMidia.Cd, 0);

            NUnit.Framework.Assert.That(result.EhValido, Is.False);
        }

        

    }
}
