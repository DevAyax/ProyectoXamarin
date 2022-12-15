// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-23-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="MockDataStore.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models;

namespace ProyectoXamarin.Services
{
	/// <summary>
	/// Class MockDataStore.
	/// Implements the <see cref="ProyectoXamarin.Interfaces.IDataStore{ProyectoXamarin.Models.Item}" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.Interfaces.IDataStore{ProyectoXamarin.Models.Item}" />
	public class MockDataStore : IDataStore<Item>
	{
		/// <summary>
		/// The items
		/// </summary>
		private readonly List<Item> items;

		/// <summary>
		/// Initializes a new instance of the <see cref="MockDataStore"/> class.
		/// </summary>
		public MockDataStore()
		{
			items = new List<Item>()
			{
				new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
				new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
				new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
				new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
				new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
				new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
			};
		}

		/// <summary>
		/// Add item as an asynchronous operation.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>A Task&lt;System.Boolean&gt; representing the asynchronous operation.</returns>
		public async Task<bool> AddItemAsync(Item item)
		{
			items.Add(item);

			return await Task.FromResult(true);
		}

		/// <summary>
		/// Update item as an asynchronous operation.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>A Task&lt;System.Boolean&gt; representing the asynchronous operation.</returns>
		public async Task<bool> UpdateItemAsync(Item item)
		{
			var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
			items.Remove(oldItem);
			items.Add(item);

			return await Task.FromResult(true);
		}

		/// <summary>
		/// Delete item as an asynchronous operation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>A Task&lt;System.Boolean&gt; representing the asynchronous operation.</returns>
		public async Task<bool> DeleteItemAsync(string id)
		{
			var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
			items.Remove(oldItem);

			return await Task.FromResult(true);
		}

		/// <summary>
		/// Get item as an asynchronous operation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>A Task&lt;Item&gt; representing the asynchronous operation.</returns>
		public async Task<Item> GetItemAsync(string id)
		{
			return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
		}

		/// <summary>
		/// Get items as an asynchronous operation.
		/// </summary>
		/// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
		/// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
		public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
		{
			return await Task.FromResult(items);
		}
	}
}