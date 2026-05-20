using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace SuzerainSaveEditor
{
    public partial class RiziaViewModel : BaseViewModel
    {
        // Economy
        [ObservableProperty] private int? _resourcesBudget;
        [ObservableProperty] private int? _resourcesAuthority;
        [ObservableProperty] private int? _resourcesEnergy;
        [ObservableProperty] private int? _budgetModifierIncomeTax;
        [ObservableProperty] private int? _energyModifierBaseEnergy;
        [ObservableProperty] private int? _authorityModifierBaseMonarchRomus;

        // Military
        [ObservableProperty] private int? _armyUnitInfantryTotal;
        [ObservableProperty] private int? _armyUnitTankTotal;
        [ObservableProperty] private int? _armyUnitSubmarineTotal;
        [ObservableProperty] private int? _armyUnitSupportTotal;
        [ObservableProperty] private int? _armyUnitShipTotal;
        [ObservableProperty] private int? _resourcesMilitaryTrucks;
        [ObservableProperty] private int? _resourcesMilitaryShips;
        [ObservableProperty] private int? _resourcesMilitaryTanks;
        [ObservableProperty] private int? _resourcesMilitarySubmarines;
        [ObservableProperty] private int? _resourcesMilitaryManpower;
        [ObservableProperty] private int? _resourcesMilitaryEquipment;
        [ObservableProperty] private int? _warActionPoints;
        [ObservableProperty] private int? _airStrikeRefillPerFragment;
        [ObservableProperty] private int? _warPalesPublicWarSupport;

        // Welfare, Opinion, Diplomacy
        [ObservableProperty] private int? _countryProductionCapacity;
        [ObservableProperty] private int? _countryLivingStandards;
        [ObservableProperty] private int? _countryPublicOpinion;
        [ObservableProperty] private int? _countryHouseToras;
        [ObservableProperty] private int? _countryHouseSazon;
        [ObservableProperty] private int? _countryHouseAzaro;
        [ObservableProperty] private int? _countryGolcondistOpinion;
        [ObservableProperty] private int? _countryWruhecistOpinion;
        [ObservableProperty] private int? _countryDastnuristOpinion;
        [ObservableProperty] private int? _diplomacyDerdia;
        [ObservableProperty] private int? _diplomacyMorella;
        [ObservableProperty] private int? _diplomacySordland;
        [ObservableProperty] private int? _zilleReferendumPower;
        [ObservableProperty] private int? _diplomacyLespia;
        [ObservableProperty] private bool _countryRelationLespiaFriendly;
        [ObservableProperty] private int? _diplomacyValgsland;
        [ObservableProperty] private int? _diplomacyPales;
        [ObservableProperty] private int? _diplomacyRumburg;
        [ObservableProperty] private int? _diplomacyWehlen;

        // Relations
        [ObservableProperty] private int? _relationsSal;
        [ObservableProperty] private int? _relationsVina;
        [ObservableProperty] private int? _relationsEstela;
        [ObservableProperty] private int? _relationsPabel;
        [ObservableProperty] private int? _relationsHugo;
        [ObservableProperty] private int? _relationsLucitaRomance;

        protected override void LoadDataToProperties()
        {
            ResourcesBudget = GetIntValue("RiziaDLC.Resources_Budget");
            ResourcesAuthority = GetIntValue("RiziaDLC.Resources_Authority");
            ResourcesEnergy = GetIntValue("RiziaDLC.Resources_Energy");
            BudgetModifierIncomeTax = GetIntValue("RiziaDLC.Budget_Modifier_IncomeTax");
            EnergyModifierBaseEnergy = GetIntValue("RiziaDLC.Energy_Modifier_BaseEnergy");
            AuthorityModifierBaseMonarchRomus = GetIntValue("RiziaDLC.Authority_Modifier_BaseMonarchRomus");

            ArmyUnitInfantryTotal = GetIntValue("RiziaDLC.Army_Unit_Infantry_Total");
            ArmyUnitTankTotal = GetIntValue("RiziaDLC.Army_Unit_Tank_Total");
            ArmyUnitSubmarineTotal = GetIntValue("RiziaDLC.Army_Unit_Submarine_Total");
            ArmyUnitSupportTotal = GetIntValue("RiziaDLC.Army_Unit_Support_Total");
            ArmyUnitShipTotal = GetIntValue("RiziaDLC.Army_Unit_Ship_Total");
            ResourcesMilitaryTrucks = GetIntValue("RiziaDLC.Resources_Military_Trucks");
            ResourcesMilitaryShips = GetIntValue("RiziaDLC.Resources_Military_Ships");
            ResourcesMilitaryTanks = GetIntValue("RiziaDLC.Resources_Military_Tanks");
            ResourcesMilitarySubmarines = GetIntValue("RiziaDLC.Resources_Military_Submarines");
            ResourcesMilitaryManpower = GetIntValue("RiziaDLC.Resources_Military_Manpower");
            ResourcesMilitaryEquipment = GetIntValue("RiziaDLC.Resources_Military_Equipment");
            WarActionPoints = GetIntValue("RiziaDLC.War_ActionPoints");
            AirStrikeRefillPerFragment = GetIntValue("RiziaDLC.Resources_Military_AirStrikeRefillPerFragment");
            WarPalesPublicWarSupport = GetIntValue("RiziaDLC.War_Pales_PublicWarSupport");

            CountryProductionCapacity = GetIntValue("RiziaDLC.Country_ProductionCapacity");
            CountryLivingStandards = GetIntValue("RiziaDLC.Country_LivingStandards");
            CountryPublicOpinion = GetIntValue("RiziaDLC.Country_PublicOpinion");
            CountryHouseToras = GetIntValue("RiziaDLC.Country_HouseToras");
            CountryHouseSazon = GetIntValue("RiziaDLC.Country_HouseSazon");
            CountryHouseAzaro = GetIntValue("RiziaDLC.Country_HouseAzaro");
            CountryGolcondistOpinion = GetIntValue("RiziaDLC.Country_GolcondistOpinion");
            CountryWruhecistOpinion = GetIntValue("RiziaDLC.Country_WruhecistOpinion");
            CountryDastnuristOpinion = GetIntValue("RiziaDLC.Country_DastnuristOpinion");
            DiplomacyDerdia = GetIntValue("RiziaDLC.Diplomacy_Derdia_NegotiatingPower");
            DiplomacyMorella = GetIntValue("RiziaDLC.Diplomacy_Morella_NegotiatingPower");
            DiplomacySordland = GetIntValue("RiziaDLC.Diplomacy_Sordland_NegotiatingPower");
            ZilleReferendumPower = GetIntValue("RiziaDLC.Zille_ReferendumPower");
            DiplomacyLespia = GetIntValue("RiziaDLC.Diplomacy_Lespia_NegotiatingPower");
            CountryRelationLespiaFriendly = GetBoolValue("RiziaDLC.CountryRelation_Lespia_Friendly");
            DiplomacyValgsland = GetIntValue("RiziaDLC.Diplomacy_Valgsland_NegotiatingPower");
            DiplomacyPales = GetIntValue("RiziaDLC.Diplomacy_Pales_NegotiatingPower");
            DiplomacyRumburg = GetIntValue("RiziaDLC.Diplomacy_Rumburg_NegotiatingPower");
            DiplomacyWehlen = GetIntValue("RiziaDLC.Diplomacy_Wehlen_NegotiatingPower");

            RelationsSal = GetIntValue("RiziaDLC.Relations_Sal");
            RelationsVina = GetIntValue("RiziaDLC.Relations_Vina");
            RelationsEstela = GetIntValue("RiziaDLC.Relations_Estela");
            RelationsPabel = GetIntValue("RiziaDLC.Relations_Pabel");
            RelationsHugo = GetIntValue("RiziaDLC.Relations_Hugo");
            RelationsLucitaRomance = GetIntValue("RiziaDLC.Relations_Lucita_Romance");
        }

        private int? GetIntValue(string key)
        {
            if (_saveData != null && _saveData.TryGetValue(key, out var val) && val is int intVal) return intVal;
            if (_saveData != null && _saveData.TryGetValue(key, out var valD) && valD is double doubleVal) return Convert.ToInt32(doubleVal);
            return null;
        }

        private bool GetBoolValue(string key)
        {
             if (_saveData != null && _saveData.TryGetValue(key, out var val) && val is bool boolVal) return boolVal;
             return false;
        }

        protected override void SavePropertiesToData()
        {
            if (_saveData == null) return;

            if (ResourcesBudget.HasValue) _saveData["RiziaDLC.Resources_Budget"] = ResourcesBudget.Value;
            if (ResourcesAuthority.HasValue) _saveData["RiziaDLC.Resources_Authority"] = ResourcesAuthority.Value;
            if (ResourcesEnergy.HasValue) _saveData["RiziaDLC.Resources_Energy"] = ResourcesEnergy.Value;
            if (BudgetModifierIncomeTax.HasValue) _saveData["RiziaDLC.Budget_Modifier_IncomeTax"] = BudgetModifierIncomeTax.Value;
            if (EnergyModifierBaseEnergy.HasValue) _saveData["RiziaDLC.Energy_Modifier_BaseEnergy"] = EnergyModifierBaseEnergy.Value;
            if (AuthorityModifierBaseMonarchRomus.HasValue) _saveData["RiziaDLC.Authority_Modifier_BaseMonarchRomus"] = AuthorityModifierBaseMonarchRomus.Value;

            if (ArmyUnitInfantryTotal.HasValue) _saveData["RiziaDLC.Army_Unit_Infantry_Total"] = ArmyUnitInfantryTotal.Value;
            if (ArmyUnitTankTotal.HasValue) _saveData["RiziaDLC.Army_Unit_Tank_Total"] = ArmyUnitTankTotal.Value;
            if (ArmyUnitSubmarineTotal.HasValue) _saveData["RiziaDLC.Army_Unit_Submarine_Total"] = ArmyUnitSubmarineTotal.Value;
            if (ArmyUnitSupportTotal.HasValue) _saveData["RiziaDLC.Army_Unit_Support_Total"] = ArmyUnitSupportTotal.Value;
            if (ArmyUnitShipTotal.HasValue) _saveData["RiziaDLC.Army_Unit_Ship_Total"] = ArmyUnitShipTotal.Value;
            if (ResourcesMilitaryTrucks.HasValue) _saveData["RiziaDLC.Resources_Military_Trucks"] = ResourcesMilitaryTrucks.Value;
            if (ResourcesMilitaryShips.HasValue) _saveData["RiziaDLC.Resources_Military_Ships"] = ResourcesMilitaryShips.Value;
            if (ResourcesMilitaryTanks.HasValue) _saveData["RiziaDLC.Resources_Military_Tanks"] = ResourcesMilitaryTanks.Value;
            if (ResourcesMilitarySubmarines.HasValue) _saveData["RiziaDLC.Resources_Military_Submarines"] = ResourcesMilitarySubmarines.Value;
            if (ResourcesMilitaryManpower.HasValue) _saveData["RiziaDLC.Resources_Military_Manpower"] = ResourcesMilitaryManpower.Value;
            if (ResourcesMilitaryEquipment.HasValue) _saveData["RiziaDLC.Resources_Military_Equipment"] = ResourcesMilitaryEquipment.Value;
            if (WarActionPoints.HasValue) _saveData["RiziaDLC.War_ActionPoints"] = WarActionPoints.Value;
            if (AirStrikeRefillPerFragment.HasValue) _saveData["RiziaDLC.Resources_Military_AirStrikeRefillPerFragment"] = AirStrikeRefillPerFragment.Value;
            if (WarPalesPublicWarSupport.HasValue) _saveData["RiziaDLC.War_Pales_PublicWarSupport"] = WarPalesPublicWarSupport.Value;

            if (CountryProductionCapacity.HasValue) _saveData["RiziaDLC.Country_ProductionCapacity"] = CountryProductionCapacity.Value;
            if (CountryLivingStandards.HasValue) _saveData["RiziaDLC.Country_LivingStandards"] = CountryLivingStandards.Value;
            if (CountryPublicOpinion.HasValue) _saveData["RiziaDLC.Country_PublicOpinion"] = CountryPublicOpinion.Value;
            if (CountryHouseToras.HasValue) _saveData["RiziaDLC.Country_HouseToras"] = CountryHouseToras.Value;
            if (CountryHouseSazon.HasValue) _saveData["RiziaDLC.Country_HouseSazon"] = CountryHouseSazon.Value;
            if (CountryHouseAzaro.HasValue) _saveData["RiziaDLC.Country_HouseAzaro"] = CountryHouseAzaro.Value;
            if (CountryGolcondistOpinion.HasValue) _saveData["RiziaDLC.Country_GolcondistOpinion"] = CountryGolcondistOpinion.Value;
            if (CountryWruhecistOpinion.HasValue) _saveData["RiziaDLC.Country_WruhecistOpinion"] = CountryWruhecistOpinion.Value;
            if (CountryDastnuristOpinion.HasValue) _saveData["RiziaDLC.Country_DastnuristOpinion"] = CountryDastnuristOpinion.Value;
            if (DiplomacyDerdia.HasValue) _saveData["RiziaDLC.Diplomacy_Derdia_NegotiatingPower"] = DiplomacyDerdia.Value;
            if (DiplomacyMorella.HasValue) _saveData["RiziaDLC.Diplomacy_Morella_NegotiatingPower"] = DiplomacyMorella.Value;
            if (DiplomacySordland.HasValue) _saveData["RiziaDLC.Diplomacy_Sordland_NegotiatingPower"] = DiplomacySordland.Value;
            if (ZilleReferendumPower.HasValue) _saveData["RiziaDLC.Zille_ReferendumPower"] = ZilleReferendumPower.Value;
            if (DiplomacyLespia.HasValue) _saveData["RiziaDLC.Diplomacy_Lespia_NegotiatingPower"] = DiplomacyLespia.Value;
            _saveData["RiziaDLC.CountryRelation_Lespia_Friendly"] = CountryRelationLespiaFriendly;
            if (DiplomacyValgsland.HasValue) _saveData["RiziaDLC.Diplomacy_Valgsland_NegotiatingPower"] = DiplomacyValgsland.Value;
            if (DiplomacyPales.HasValue) _saveData["RiziaDLC.Diplomacy_Pales_NegotiatingPower"] = DiplomacyPales.Value;
            if (DiplomacyRumburg.HasValue) _saveData["RiziaDLC.Diplomacy_Rumburg_NegotiatingPower"] = DiplomacyRumburg.Value;
            if (DiplomacyWehlen.HasValue) _saveData["RiziaDLC.Diplomacy_Wehlen_NegotiatingPower"] = DiplomacyWehlen.Value;

            if (RelationsSal.HasValue) _saveData["RiziaDLC.Relations_Sal"] = RelationsSal.Value;
            if (RelationsVina.HasValue) _saveData["RiziaDLC.Relations_Vina"] = RelationsVina.Value;
            if (RelationsEstela.HasValue) _saveData["RiziaDLC.Relations_Estela"] = RelationsEstela.Value;
            if (RelationsPabel.HasValue) _saveData["RiziaDLC.Relations_Pabel"] = RelationsPabel.Value;
            if (RelationsHugo.HasValue) _saveData["RiziaDLC.Relations_Hugo"] = RelationsHugo.Value;
            if (RelationsLucitaRomance.HasValue) _saveData["RiziaDLC.Relations_Lucita_Romance"] = RelationsLucitaRomance.Value;
        }
    }
}