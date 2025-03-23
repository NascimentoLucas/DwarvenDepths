using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Nascimento.Model;
using System;

public class LevelSOUnityTest
{
    Dictionary<string, LevelSO> levelSOs;
    Dictionary<string, ItemSO> itens;
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        levelSOs = new Dictionary<string, LevelSO>();

        itens = new Dictionary<string, ItemSO>();
        string[] guids = AssetDatabase.FindAssets("t:LevelSO");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            LevelSO levelSO = AssetDatabase.LoadAssetAtPath<LevelSO>(path);
            Debug.Assert(levelSO != null, $"LevelSO not found at path: {path}");
            if (levelSO != null)
            {
                levelSOs.Add(levelSO.GetInstanceID().ToString(), levelSO);
                itens.Add(levelSO.Item.GetInstanceID().ToString(), levelSO.Item);
            }
        }
    }

    [UnityTest]
    public IEnumerator CheckForMissingItems()
    {
        foreach (var levelSO in levelSOs)
        {
            foreach (var component in levelSO.Value.Item.Components)
            {
                if (!itens.ContainsKey(component.Item.GetInstanceID().ToString()))
                {
                    var msg = $"Item {levelSO.Value.Item} precisa do(a) {component.Item.name} mas ele não está em nenhum level do jogo";
                    Debug.Log(msg, levelSO.Value);
                    Assert.Fail(msg);
                }
            }
        }
        Assert.IsTrue(true);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckForZeroAmount()
    {
        foreach (var levelSO in levelSOs)
        {
            foreach (var component in levelSO.Value.Item.Components)
            {
                if (component.Amount < 1)
                {
                    var msg = $"Item {levelSO.Value.Item} precisa de {component.Item.name} mas a quantidade é 0";
                    Debug.Log(msg, levelSO.Value.Item);
                    Assert.Fail(msg);
                }
            }
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckForDescriptionError()
    {
        string s;
        foreach (var item in itens)
        {
            try
            {
                s = item.Value.Description;
            }
            catch (FormatException)
            {
                var msg = $"Item {item.Value.name} tem um erro na descrição";
                Debug.Log(msg, item.Value);
                Assert.Fail(msg);
            }
        }
        yield return null;
    }
}
