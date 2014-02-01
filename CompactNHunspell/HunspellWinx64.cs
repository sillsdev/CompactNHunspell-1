﻿//------------------------------------------------------------------------------
// <copyright 
//  file="HunspellWinx64.cs" 
//  company="enckse">
//  Copyright (c) All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
namespace CompactNHunspell
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
 
    /// <summary>
    /// Hunspell for windows x64
    /// </summary>
    internal class HunspellWinx64 : BaseHunspell
    {
        /// <summary>
        /// Library for the reference assembly
        /// </summary>
        private const string Library = "Hunspellx64.dll";
        
        /// <summary>
        /// Hunspell free.
        /// </summary>
        /// <param name='handle'>
        /// Handle to release.
        /// </param>
        [DllImport(Library)]
        public static extern void HunspellFree(IntPtr handle);
  
        /// <summary>
        /// Initializes Hunspell.
        /// </summary>
        /// <returns>
        /// Pointer to the instance
        /// </returns>
        /// <param name='affixData'>
        /// Affix data.
        /// </param>
        /// <param name='affixDataSize'>
        /// Affix data size.
        /// </param>
        /// <param name='dictionaryData'>
        /// Dictionary data.
        /// </param>
        /// <param name='dictionaryDataSize'>
        /// Dictionary data size.
        /// </param>
        /// <param name='key'>
        /// Key if encrypted.
        /// </param>
        [DllImport(Library)]
        public static extern IntPtr HunspellInit([MarshalAs(UnmanagedType.LPArray)] byte[] affixData, IntPtr affixDataSize, [MarshalAs(UnmanagedType.LPArray)] byte[] dictionaryData, IntPtr dictionaryDataSize, string key);
  
        /// <summary>
        /// Check the spelling of a word
        /// </summary>
        /// <returns>
        /// True if the word is spelled correctly
        /// </returns>
        /// <param name='handle'>
        /// Instance handle
        /// </param>
        /// <param name='word'>
        /// Word to check
        /// </param>
        [DllImport(Library)]
        public static extern bool HunspellSpell(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string word);
        
        /// <summary>
        /// Add the word to the instance
        /// </summary>
        /// <param name='handle'>
        /// Instance handle
        /// </param>
        /// <param name='word'>
        /// Word to add
        /// </param>
        /// <returns>
        /// True if word is added
        /// </returns>
        [DllImport(Library)]
        public static extern bool HunspellAdd(IntPtr handle, string word);
        
        /// <summary>
        /// Free the specified handle.
        /// </summary>
        /// <param name='handle'>
        /// Handle to free.
        /// </param>
        protected override void Free(IntPtr handle)
        {
            this.WriteTraceMessage("Freeing pointer");
            this.WriteTraceMessage(handle.ToString());
            HunspellFree(handle);
            this.WriteTraceMessage("Freeing pointer completed");
        }
        
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <returns>
        /// The instance.
        /// </returns>
        /// <param name='affFile'>
        /// Affix file.
        /// </param>
        /// <param name='dictFile'>
        /// Dict file.
        /// </param>
        protected override IntPtr InitInstance(string affFile, string dictFile)
        {
            this.WriteTraceMessage("Creating the instance");
            this.WriteTraceMessage(affFile);
            this.WriteTraceMessage(dictFile);
            return this.WindowsInit(affFile, dictFile);
        }
  
        /// <summary>
        /// Invoke the instance with data
        /// </summary>
        /// <returns>
        /// The instance pointer.
        /// </returns>
        /// <param name='affixData'>
        /// Affix data.
        /// </param>
        /// <param name='affixSize'>
        /// Affix size.
        /// </param>
        /// <param name='dictData'>
        /// Dict data.
        /// </param>
        /// <param name='dictSize'>
        /// Dict size.
        /// </param>
        protected override IntPtr DataInvoke(byte[] affixData, IntPtr affixSize, byte[] dictData, IntPtr dictSize)
        {
            this.WriteTraceMessage("Calling DataInvoke");
            this.WriteTraceMessage(affixSize.ToString());
            this.WriteTraceMessage(dictSize.ToString());
            return HunspellInit(affixData, affixSize, dictData, dictSize, null);
        }
        
        /// <summary>
        /// Spell check the word
        /// </summary>
        /// <param name='handle'>
        /// Handle to use
        /// </param>
        /// <param name='word'>
        /// Word to check
        /// </param>
        /// <returns>True if the word is properly spelled</returns>
        protected override bool Spell(IntPtr handle, string word)
        {
            this.WriteTraceMessage("Performing spell check");
            this.WriteTraceMessage(handle.ToString());
            this.WriteTraceMessage(word);
            return HunspellSpell(handle, word);
        }
        
        /// <summary>
        /// Adds the word to the dictionary
        /// </summary>
        /// <param name='pointer'>
        /// Pointer to the instance
        /// </param>
        /// <param name='word'>
        /// Word to add
        /// </param>
        protected override void AddWord(IntPtr pointer, string word)
        {
            this.WriteTraceMessage("Adding word");
            this.WriteTraceMessage(pointer.ToString());
            this.WriteTraceMessage(word);
            HunspellAdd(pointer, word);
            this.WriteTraceMessage("Word added");
        }
    }
}
