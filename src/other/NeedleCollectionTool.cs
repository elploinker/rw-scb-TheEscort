using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using RWCustom;
using System.IO;
using TheEscort;  // replace this with whatever namespace you have your Plugin in

namespace SpearmasterNeedleDataCollectionTool;
static class NeedleLogger
{
    /// <summary>
    /// let me have this. please. (Replacement for a Tuple since it's a goddamn readonly)
    /// </summary>
    public class NeedleCounter
    {
        public readonly string region;
        public int nCreate, nDrop, nThrow;

        public NeedleCounter(string region, int nCreate = 0, int nDrop = 0, int nThrow = 0)
        {
            this.region = region;
            this.nCreate = nCreate;
            this.nDrop = nDrop;
            this.nThrow = nThrow;
        }
    }

    /// <summary>
    /// Stores data on the creation/drop/throw of spearmaster needles during the campaign to allow more accurate needle spawn chance.
    /// </summary>
    public class NeedleMe
    {
        protected List<NeedleRecord> RecordOfNeedles {get; private set;} = new();
        private int cycleNo;
        private bool needsRenewal;

        /// <summary>
        /// constructor for needle data collector. Stop thinking what you are thinking in your dirty mind.
        /// </summary>
        public NeedleMe(int cycleNum)
        {
            cycleNo = cycleNum;
        } 

        public void Capture(in Player player, bool isCreate = false, bool isDrop = false, bool isThrow = false)
        {
            try
            {
                if (needsRenewal) throw new Exception("->NEEDLOG>>>LOGGER NEEDS RENEWAL");
                RecordOfNeedles.Add(new(cycleNo, player.room.world.region.name, player.room.abstractRoom.name, isCreate, isDrop, isThrow));
            }
            catch (Exception err)
            {
                UnityEngine.Debug.LogError(err);
                UnityEngine.Debug.LogWarning("->NEEDLOG>>>FAILURE TO ADD ENTRY");
            }
        }
        
        public void Release(bool successfulCycle)
        {
            string fileName = "deathpits_needs_some_info_" + cycleNo.ToString("000") + ".csv";
            string filePath = AssetManager.ResolveDirectory("DeathpitsDataCollectingCo");
            string lastRegion = "";
            Queue<Dictionary<string, NeedleCounter>> things = new();

            // Analyse the results
            foreach (NeedleRecord nr in RecordOfNeedles)
            {
                if (lastRegion != nr.regionName)
                {
                    things.Enqueue(new());
                }

                if (!things.Peek().ContainsKey(nr.roomName))
                {
                    things.Peek().Add(nr.roomName, new(nr.regionName, 0, 0, 0));
                }

                if (nr.isCreate) things.Peek()[nr.roomName].nCreate++;
                if (nr.isDrop) things.Peek()[nr.roomName].nDrop++;
                if (nr.isThrow) things.Peek()[nr.roomName].nThrow++;
            }

            string prtTxt = "Cycle,Success,Region,Room,Creations,Drops,Throws\r\n";
            while (things.Count > 0)
            {
                foreach(KeyValuePair<string, NeedleCounter> v in things.Dequeue())
                {
                    prtTxt += $"{cycleNo},{successfulCycle},{v.Value.region},{v.Key},{v.Value.nCreate},{v.Value.nDrop},{v.Value.nThrow}\r\n";
                }
            }
            // Create folder (if doesn't exist)
            Directory.CreateDirectory(filePath);            

            // Create file
            filePath = AssetManager.ResolveFilePath("DeathpitsDataCollectingCo/" + fileName);
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.Write(prtTxt);
            }
            UnityEngine.Debug.LogWarning("->NEEDLOG>>>Needle Recorded!");
            needsRenewal = true;
        }

        public void Renew(int cycleNum)
        {
            cycleNo = cycleNum;
            RecordOfNeedles.Clear();
            needsRenewal = false;
        }
    }

    /// <summary>
    /// Conveniently stores each entry to allow easy CSV printing
    /// </summary>
    public record NeedleRecord
    {
        public readonly int cycleCount;
        public readonly bool isThrow, isDrop, isCreate;
        public readonly string regionName, roomName;
        private const string separator = "<S>";

        public NeedleRecord(int cycleCount, string regionName = "", string roomName = "", bool isCreate = false, bool isDrop = false, bool isThrow = false)
        {
            this.cycleCount = cycleCount;
            this.regionName = regionName;
            this.roomName = roomName;
            this.isCreate = isCreate;
            this.isDrop = isDrop;
            this.isThrow = isThrow;
        }
    }
}