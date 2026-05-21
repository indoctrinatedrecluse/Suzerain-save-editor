using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Graphics;
using System;

namespace SuzerainSaveEditor
{
    public partial class BaseGameViewModel : BaseViewModel
    {
        // Core & Opinion
        [ObservableProperty] 
        [NotifyPropertyChangedFor(nameof(BudgetTextColor))]
        private int? _governmentBudget;
        
        [ObservableProperty] private int? _personalWealth;
        [ObservableProperty] private int? _countryUnrest;
        [ObservableProperty] private int? _publicOpinion;
        [ObservableProperty] private int? _bludishOpinion;
        [ObservableProperty] private int? _gameTurn;
        [ObservableProperty] private string? _budgetWarningText;

        public Color BudgetTextColor => (GameTurn.HasValue && GameTurn < 4 && GovernmentBudget.HasValue && GovernmentBudget > 3) ? Colors.Red : Colors.Black;


        // Politics & Reform
        [ObservableProperty] private int? _reformAssemblyVote;
        [ObservableProperty] private int? _reformCourtVote;
        [ObservableProperty] private bool _albinConvinced;
        [ObservableProperty] private bool _gloriaConvinced;
        [ObservableProperty] private bool _turn04AConvinceGloria;
        [ObservableProperty] private bool _turn04AConvinceAlbin;
        [ObservableProperty] private bool _amendmentUspGloriaConvinced;
        [ObservableProperty] private bool _factionUspAgainstProposal;

        // Economy & Trade
        [ObservableProperty] private int? _economyAgnland;
        [ObservableProperty] private int? _economyBergia;
        [ObservableProperty] private int? _economyLorren;
        [ObservableProperty] private int? _economyGruni;
        [ObservableProperty] private int? _tradeAmount;

        // Relations & Prologue
        [ObservableProperty] private int? _relationsFrancOpinion;
        [ObservableProperty] private int? _relationsMonicaOpinion;
        [ObservableProperty] private int? _relationsLucianOpinion;
        [ObservableProperty] private bool _prologueStudyplaceDeyrHistory;
        [ObservableProperty] private bool _prologueStudyplaceHolsordLaw;
        [ObservableProperty] private bool _prologueStudyplaceLachavenEconomics;
        [ObservableProperty] private int? _leakedScandalsSeverity;
        [ObservableProperty] private bool _televisionInterviewEmpathy;

        // Military Expansion & Modernisation
        [ObservableProperty] private bool _turn06SnOExpansionArmy;
        [ObservableProperty] private bool _turn06SnOExpansionNavy;
        [ObservableProperty] private bool _turn06SnOExpansionAirforce;
        [ObservableProperty] private bool _situationMilitaryExpandedArmy;
        [ObservableProperty] private bool _situationMilitaryExpandedNavy;
        [ObservableProperty] private bool _situationMilitaryExpandedAirForce;
        
        [ObservableProperty] private bool _turn06SnOModernisationArmy;
        [ObservableProperty] private bool _turn06SnOModernisationNavy;
        [ObservableProperty] private bool _turn06SnOModernisationAirforce;
        [ObservableProperty] private bool _situationMilitaryModernisedArmy;
        [ObservableProperty] private bool _situationMilitaryModernisedNavy;
        [ObservableProperty] private bool _situationMilitaryModernisedAirForce;

        protected override void LoadDataToProperties()
        {
            GameTurn = GetIntValue("Game.Turn");
            GovernmentBudget = GetIntValue("BaseGame.GovernmentBudget");
            PersonalWealth = GetIntValue("BaseGame.PersonalWealth");
            CountryUnrest = GetIntValue("BaseGame.Country_Unrest");
            PublicOpinion = GetIntValue("BaseGame.Public_Opinion");
            BludishOpinion = GetIntValue("BaseGame.Bludish_Opinion");

            ReformAssemblyVote = GetIntValue("BaseGame.Reform_Assembly_Vote");
            ReformCourtVote = GetIntValue("BaseGame.Reform_Court_Vote");
            AlbinConvinced = GetBoolValue("Albin_Convinced");
            GloriaConvinced = GetBoolValue("Gloria_Convinced");
            Turn04AConvinceGloria = GetBoolValue("GameCondition.Turn04_A_Convince_Gloria");
            Turn04AConvinceAlbin = GetBoolValue("GameCondition.Turn04_A_Convince_Albin");
            AmendmentUspGloriaConvinced = GetBoolValue("BaseGame.Amendment_USP_Gloria_Convinced");
            FactionUspAgainstProposal = GetBoolValue("BaseGame.Faction_USP_AgainstProposal");

            EconomyAgnland = GetIntValue("BaseGame.Economy_Agnland");
            EconomyBergia = GetIntValue("BaseGame.Economy_Bergia");
            EconomyLorren = GetIntValue("BaseGame.Economy_Lorren");
            EconomyGruni = GetIntValue("BaseGame.Economy_Gruni");
            TradeAmount = GetIntValue("BaseGame.TradeAmount");

            RelationsFrancOpinion = GetIntValue("BaseGame.Relations_Franc_Opinion");
            RelationsMonicaOpinion = GetIntValue("BaseGame.Relations_Monica_Opinion");
            RelationsLucianOpinion = GetIntValue("BaseGame.Relations_Lucian_Opinion");
            PrologueStudyplaceDeyrHistory = GetBoolValue("BaseGame.Prologue_Studyplace_Deyr_History");
            PrologueStudyplaceHolsordLaw = GetBoolValue("BaseGame.Prologue_Studyplace_Holsord_Law");
            PrologueStudyplaceLachavenEconomics = GetBoolValue("BaseGame.Prologue_Studyplace_Lachaven_Economics");
            LeakedScandalsSeverity = GetIntValue("BaseGame.LeakedScandals_Severity");
            TelevisionInterviewEmpathy = GetBoolValue("BaseGameIsolated.Turn09_A_TelevisionInterview_International_Empathy_Gained");

            Turn06SnOExpansionArmy = GetBoolValue("BaseGame.Turn06_SnO_Expansion_Army");
            Turn06SnOExpansionNavy = GetBoolValue("BaseGame.Turn06_SnO_Expansion_Navy");
            Turn06SnOExpansionAirforce = GetBoolValue("BaseGame.Turn06_SnO_Expansion_Airforce");
            SituationMilitaryExpandedArmy = GetBoolValue("BaseGame.Situation_Military_ExpandedArmy");
            SituationMilitaryExpandedNavy = GetBoolValue("BaseGame.Situation_Military_ExpandedNavy");
            SituationMilitaryExpandedAirForce = GetBoolValue("BaseGame.Situation_Military_ExpandedAirForce");

            Turn06SnOModernisationArmy = GetBoolValue("BaseGame.Turn06_SnO_Modernisation_Army");
            Turn06SnOModernisationNavy = GetBoolValue("BaseGame.Turn06_SnO_Modernisation_Navy");
            Turn06SnOModernisationAirforce = GetBoolValue("BaseGame.Turn06_SnO_Modernisation_Airforce");
            SituationMilitaryModernisedArmy = GetBoolValue("BaseGame.Situation_Military_ModernisedArmy");
            SituationMilitaryModernisedNavy = GetBoolValue("BaseGame.Situation_Military_ModernisedNavy");
            SituationMilitaryModernisedAirForce = GetBoolValue("BaseGame.Situation_Military_ModernisedAirForce");
            
            UpdateBudgetWarning();
        }

        partial void OnGovernmentBudgetChanged(int? value)
        {
            UpdateBudgetWarning();
        }

        private void UpdateBudgetWarning()
        {
            if (GameTurn.HasValue && GameTurn < 4 && GovernmentBudget.HasValue && GovernmentBudget > 3)
            {
                BudgetWarningText = "Warning: Setting budget > 3 before Turn 4 can cause Black Tuesday!";
            }
            else
            {
                BudgetWarningText = null;
            }
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

            if (GovernmentBudget.HasValue) _saveData["BaseGame.GovernmentBudget"] = GovernmentBudget.Value;
            if (PersonalWealth.HasValue) _saveData["BaseGame.PersonalWealth"] = PersonalWealth.Value;
            if (CountryUnrest.HasValue) _saveData["BaseGame.Country_Unrest"] = CountryUnrest.Value;
            if (PublicOpinion.HasValue) _saveData["BaseGame.Public_Opinion"] = PublicOpinion.Value;
            if (BludishOpinion.HasValue) _saveData["BaseGame.Bludish_Opinion"] = BludishOpinion.Value;

            if (ReformAssemblyVote.HasValue) _saveData["BaseGame.Reform_Assembly_Vote"] = ReformAssemblyVote.Value;
            if (ReformCourtVote.HasValue) _saveData["BaseGame.Reform_Court_Vote"] = ReformCourtVote.Value;
            _saveData["Albin_Convinced"] = AlbinConvinced;
            _saveData["Gloria_Convinced"] = GloriaConvinced;
            _saveData["GameCondition.Turn04_A_Convince_Gloria"] = Turn04AConvinceGloria;
            _saveData["GameCondition.Turn04_A_Convince_Albin"] = Turn04AConvinceAlbin;
            _saveData["BaseGame.Amendment_USP_Gloria_Convinced"] = AmendmentUspGloriaConvinced;
            _saveData["BaseGame.Faction_USP_AgainstProposal"] = FactionUspAgainstProposal;

            if (EconomyAgnland.HasValue) _saveData["BaseGame.Economy_Agnland"] = EconomyAgnland.Value;
            if (EconomyBergia.HasValue) _saveData["BaseGame.Economy_Bergia"] = EconomyBergia.Value;
            if (EconomyLorren.HasValue) _saveData["BaseGame.Economy_Lorren"] = EconomyLorren.Value;
            if (EconomyGruni.HasValue) _saveData["BaseGame.Economy_Gruni"] = EconomyGruni.Value;
            if (TradeAmount.HasValue) _saveData["BaseGame.TradeAmount"] = TradeAmount.Value;

            if (RelationsFrancOpinion.HasValue) _saveData["BaseGame.Relations_Franc_Opinion"] = RelationsFrancOpinion.Value;
            if (RelationsMonicaOpinion.HasValue) _saveData["BaseGame.Relations_Monica_Opinion"] = RelationsMonicaOpinion.Value;
            if (RelationsLucianOpinion.HasValue) _saveData["BaseGame.Relations_Lucian_Opinion"] = RelationsLucianOpinion.Value;
            _saveData["BaseGame.Prologue_Studyplace_Deyr_History"] = PrologueStudyplaceDeyrHistory;
            _saveData["BaseGame.Prologue_Studyplace_Holsord_Law"] = PrologueStudyplaceHolsordLaw;
            _saveData["BaseGame.Prologue_Studyplace_Lachaven_Economics"] = PrologueStudyplaceLachavenEconomics;
            if (LeakedScandalsSeverity.HasValue) _saveData["BaseGame.LeakedScandals_Severity"] = LeakedScandalsSeverity.Value;
            _saveData["BaseGameIsolated.Turn09_A_TelevisionInterview_International_Empathy_Gained"] = TelevisionInterviewEmpathy;

            _saveData["BaseGame.Turn06_SnO_Expansion_Army"] = Turn06SnOExpansionArmy;
            _saveData["BaseGame.Turn06_SnO_Expansion_Navy"] = Turn06SnOExpansionNavy;
            _saveData["BaseGame.Turn06_SnO_Expansion_Airforce"] = Turn06SnOExpansionAirforce;
            _saveData["BaseGame.Situation_Military_ExpandedArmy"] = SituationMilitaryExpandedArmy;
            _saveData["BaseGame.Situation_Military_ExpandedNavy"] = SituationMilitaryExpandedNavy;
            _saveData["BaseGame.Situation_Military_ExpandedAirForce"] = SituationMilitaryExpandedAirForce;

            _saveData["BaseGame.Turn06_SnO_Modernisation_Army"] = Turn06SnOModernisationArmy;
            _saveData["BaseGame.Turn06_SnO_Modernisation_Navy"] = Turn06SnOModernisationNavy;
            _saveData["BaseGame.Turn06_SnO_Modernisation_Airforce"] = Turn06SnOModernisationAirforce;
            _saveData["BaseGame.Situation_Military_ModernisedArmy"] = SituationMilitaryModernisedArmy;
            _saveData["BaseGame.Situation_Military_ModernisedNavy"] = SituationMilitaryModernisedNavy;
            _saveData["BaseGame.Situation_Military_ModernisedAirForce"] = SituationMilitaryModernisedAirForce;
        }
    }
}