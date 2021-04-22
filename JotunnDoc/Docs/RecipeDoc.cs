﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jotunn.Managers;
using UnityEngine;

namespace JotunnDoc.Docs
{
    public class RecipeDoc : Doc
    {
        public RecipeDoc() : base("JotunnDoc/Docs/conceptual/objects/recipe-list.md")
        {
            ItemManager.Instance.OnItemsRegistered += DocRecipes;
        }

        private void DocRecipes(object sender, EventArgs e)
        {
            Debug.Log("Documenting recipes");

            AddHeader(1, "Recipe list");
            AddText("All of the recipes currently in the game, with English localizations applied");
            AddText("This file is automatically generated from Valheim using the JotunnDoc mod found on our GitHub.");
            AddTableHeader("Name", "Item name", "Amount", "Resources required");

            foreach (Recipe recipe in ObjectDB.instance.m_recipes)
            {
                if (recipe == null)
                {
                    continue;
                }

                string resources = "<ul>";

                foreach (Piece.Requirement req in recipe.m_resources)
                {
                    resources += "<li>" + req.m_amount + " " +
                        JotunnDoc.Localize(req?.m_resItem?.m_itemData?.m_shared?.m_name) + "</li>";
                }

                resources += "</ul>";

                AddTableRow(
                    recipe.name,
                    JotunnDoc.Localize(recipe?.m_item?.m_itemData?.m_shared?.m_name),
                    recipe.m_amount.ToString(),
                    resources);
            }

            Save();
            Debug.Log("\t-> Done");
        }
    }
}
