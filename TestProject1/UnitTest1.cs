namespace boggle
{
    [TestClass]
    public class UnitTest1
    {
        #region Test de la methode Contains
        [TestMethod]
        public void TestMethodContains()
        {
            Joueur joueur = new Joueur("test");
            joueur.AddMot("abricot");
            joueur.AddMot("banane");
            joueur.AddMot("peche");
            bool result = joueur.Contains("banane");
            Assert.AreEqual(true, result);
        }
        #endregion

        #region Test de la méthode TriRapideDico
        [TestMethod]
        public void TestMethodTriRapide()
        {
            string[] dictionnaire = new string[] { "peche", "abricot", "banane" };
            TriDictionnaire.TriRapideDico(dictionnaire, 0, dictionnaire.Length - 1);
            string[] expected = new string[] { "abricot", "banane", "peche" };
            CollectionAssert.AreEqual(expected, dictionnaire);
        }
        #endregion

        #region Test de la méthode TriFusion
        [TestMethod]
        public void TestMethodTriFusion()
        {
            string[] dictionnaire = new string[] { "peche", "abricot", "banane" };
            TriDictionnaire.TriFusion(dictionnaire);
            string[] expected = new string[] { "abricot", "banane", "peche" };
            CollectionAssert.AreEqual(expected, dictionnaire);
        }
        #endregion

        #region Test de la méthode TriInsertion
        [TestMethod]
        public void TestMethodTriInsertion()
        {
            string[] dictionnaire = new string[] { "peche", "abricot", "banane" };
            TriDictionnaire.TriInsertion(dictionnaire);
            string[] expected = new string[] { "abricot", "banane", "peche" };
            CollectionAssert.AreEqual(expected, dictionnaire);
        }
        #endregion

        #region Test de la méthode UpdateScore
        [TestMethod]
        public void TestMethodUpdateScore()
        {
            Joueur joueur = new Joueur("test");
            
            Assert.AreEqual("test : 0 points", joueur.toString());
        }
        #endregion
    }
}