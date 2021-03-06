﻿using System;
using System.Collections.Generic;
using UnityEngine;
using KSP.IO;

namespace Nereid
{
    namespace FinalFrontier
    {
        class AchievementRecorder
        {
            private readonly HallOfFame hallOfFame;

            public AchievementRecorder()
            {
                hallOfFame = HallOfFame.Instance();
            }

            public void RecordVesselRecovered(ProtoVessel vessel)
            {
                List<ProtoCrewMember> crew = vessel.GetVesselCrew();
                foreach (ProtoCrewMember member in crew)
                {
                   if (!member.IsTourist())
                   {
                      Log.Info("kerbal " + member.name + " ended a mission ");
                      hallOfFame.RecordMissionFinished(member);
                   }
                }
            }

            public void RecordLaunch(Vessel vessel)
            {
               List<ProtoCrewMember> crew = vessel.GetVesselCrew();
               foreach (ProtoCrewMember member in crew)
               {
                  if (!member.IsTourist())
                  {
                     Log.Info("kerbal " + member.name + " launched");
                     hallOfFame.RecordLaunch(member);
                  }
               }
            }

            public void RecordEva(ProtoCrewMember crew, Vessel fromVessel)
            {
               Log.Detail("kerbal " + crew + " on EVA");
               hallOfFame.RecordEva(crew, fromVessel);
            }

            public void RecordBoarding(ProtoCrewMember crew)
            {
               Log.Detail("kerbal " + crew + " returns from EVA");
               hallOfFame.RecordBoarding(crew);
            }

            public void RecordDocking(Vessel vessel)
            {
               List<ProtoCrewMember> crew = vessel.GetVesselCrew();
               foreach (ProtoCrewMember member in crew)
               {
                  Log.Info("kerbal " + member.name + " docked");
                  hallOfFame.RecordDocking(member);
               }
            }

            public void Record(Ribbon ribbon, ProtoCrewMember kerbal)
            {
                Achievement achievement = ribbon.GetAchievement();
                hallOfFame.Record(kerbal, ribbon);
            }


            public void Record(Ribbon ribbon, Vessel vessel)
            {
               if(vessel==null)
               {
                  Log.Warning("no vessel for recorded ribbon "+ribbon.GetName());
                  return;
               }
               if (Log.IsLogable(Log.LEVEL.DETAIL)) Log.Detail("recording ribbon "+ribbon.GetName()+" for vessel "+vessel.GetName());
               List<ProtoCrewMember> crew = vessel.GetVesselCrew();
               if (crew != null)
               {
                  hallOfFame.BeginArwardOfRibbons();
                  foreach (ProtoCrewMember member in crew)
                  {
                     if(!member.IsTourist())
                     {
                        Record(ribbon, member);
                     }
                  }
                  hallOfFame.EndArwardOfRibbons();
               }
            }
        }

    }
}