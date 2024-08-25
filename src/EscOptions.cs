using Menu.Remix;
using Menu.Remix.MixedUI;
using Menu.Remix.MixedUI.ValueTypes;
using System;
using System.Collections.Generic;
using UnityEngine;
using static TheEscort.Eshelp;


namespace TheEscort
{
    /*
    class EscCusOpt : Dialog, SelectOneButton.SelectOneButtonOwner
    {
        public MenuIllustration title;
        public SimpleButton cancelButton;
        public float leftAnchor, rightAnchor;
        public bool opening, closing;
        public float movementCounter;
        public SelectOneButton[] topicButtons;
        public MenuLabel pageLabel;
        public ManualPage currentTopicPage;
        public int index, pageNumber;
        public String currentTopic;
        public Dictionary<string, int> topics;
        public float sin;
        public bool firstView;
        public float lastAlpha;
        public float currentAlpha;
        public float uAlpha;
        public float targetAlpha;
        public float globalOffX;
        public float contentOffX;
        public float wrapTextMargin;

        public EscCusOpt(ProcessManager manager, Dictionary<string, int> topics, MenuObject owner) : base(manager)
        {
            float[] screenOffsets = RWCustom.Custom.GetScreenOffsets();
            leftAnchor = screenOffsets[0]; rightAnchor = screenOffsets[1];
            this.topics = topics;
            pages[0].pos = new Vector2(0.01f, 0f);
            pages[0].pos.y += 2000f;
            pages.Add(new Page(this, owner, "CLASS", 1));
            pages[1].pos = new Vector2(520.01f, 155f);
            pages[1].pos.y += 2155f;

        }

        public int GetCurrentlySelectedOfSeries(string series)
        {
            throw new NotImplementedException();
        }

        public void SetCurrentlySelectedOfSeries(string series, int to)
        {
            throw new NotImplementedException();
        }
    }*/

    class EscOptions : OptionInterface
    {
        //public readonly Plugin instance;
        public readonly RainWorld rainworld;
        public Configurable<bool> cfgMeanLizards;
        public Configurable<bool> cfgVengefulLizards;
        // public Configurable<bool> cfgMeanGarbWorms;
        public Configurable<float> cfgHeavyLift;
        public Configurable<float> cfgDKMult;
        public Configurable<bool> cfgElevator;
        public Configurable<bool> cfgHypable;
        public Configurable<int> cfgHypeReq;
        public Configurable<float> cfgHypeRequirement;
        public Configurable<bool> cfgSFX;
        public Configurable<bool> cfgPounce;
        public Configurable<bool> cfgLongWallJump;
        //[Obsolete] public Configurable<int> cfgBuildNum;  // LEGACY!
        //[Obsolete] public Configurable<int> cfgBuildP1, cfgBuildP2, cfgBuildP3, cfgBuildP4;  // LEGACY #2
        public Configurable<int>[] cfgBuild;
        //[Obsolete] public Configurable<bool> cfgEasyP1, cfgEasyP2, cfgEasyP3, cfgEasyP4;  // LEGACY #2
        public Configurable<bool>[] cfgEasy;
        //[Obsolete] public Configurable<bool> cfgCustomP1, cfgCustomP2, cfgCustomP3, cfgCustomP4;
        public Configurable<bool> cfgDunkin;
        public Configurable<bool> cfgSpears;
        public Configurable<bool> cfgDKAnimation;
        public Configurable<bool> cfgNoticeHype;
        public Configurable<bool> cfgNoticeEmpower;
        public Configurable<bool> cfgFunnyDeflSlide;
        public Configurable<bool> cfgPoleBounce;
        public Configurable<bool> cfgOldSpeedster;
        public Configurable<bool> cfgOldEscapist;
        public Configurable<bool> cfgDeveloperMode;
        public Configurable<int> cfgSecret;
        public Configurable<bool> cfgSectret, cfgSectretBuild, cfgSectretGod, cfgSectretMagic;
        public Configurable<float> cfgEscLaunchV, cfgEscLaunchH, cfgEscLaunchSH;
        public Configurable<int> cfgLogImportance;
        public Configurable<string> cfgShowHud;
        public List<ListItem> hudShowOptions;
        public Configurable<string> cfgHudLocation;
        public List<ListItem> hudLocaOptions;
        public Configurable<bool> cfgNoMoreFlips;
        public Configurable<bool> cfgDeflecterSharedPool;
        public Configurable<bool> cfgAllBuildsGetPup;
        public Configurable<bool> sctTestBuild;
        public Configurable<int> cfgSpeedsterGears;
        //private OpLabel sctTestBuildText;
        private OpTextBox secretText;
        //private OpCheckBox hypableBtn;
        //private OpSliderTick hypedSlide;
        private OpCheckBox hypeableBox;
        private OpSliderTick hypeableTick;
        private OpLabel[] hypeableText;
        private OpCheckBox sillySFX;
        private OpCheckBox shutUpFlip;
        private OpLabel shutUpFlipText;
        private UIelement[] mainSet;
        private UIelement[] buildSet, buildTitle, buildText, buildShadow;
        //private OpCheckBox buildEasyP1, buildEasyP2, buildEasyP3, buildEasyP4;
        public OpCheckBox[] buildEasy;
        //private OpSliderTick buildP1, buildP2, buildP3, buildP4;
        public OpSliderTick[] buildPlayer;
        private OpDragger buildDragger;
        private readonly Configurable<int> buildDraggerHelper;
        private OpComboBox buildSelect;
        private readonly Configurable<string> buildSelectHelper;
        private OpCheckBox easySelect;
        private readonly Configurable<bool> easySelectHelper;
        private OpLabel buildManyCats;
        public List<ListItem> buildItems;
        private OpDragger bindDragger;
        private readonly Configurable<int> bindDraggerHelper;
        private OpCheckBox bindSelect;
        private readonly Configurable<bool> bindSelectHelper;
        private OpKeyBinder bindKey;
        private readonly Configurable<KeyCode> bindKeyHelper;
        private OpSimpleButton bindReset;
        private OpLabel bindText;
        public Configurable<KeyCode>[] cfgBindKeys;
        public Configurable<bool>[] cfgCustomBinds;
        public OpKeyBinder[] cfgBindKeysContainer;
        public OpCheckBox[] cfgCustomBindsContainer;
        private UIelement[] gimmickSet;
        private UIelement[] accessibleSet;
        private Color[] buildColors;
        private Color p1Color, p2Color, p3Color, p4Color;
        private Color tempColor;
        public int PlayerCount {get; private set;} = 4;
        private bool saitBeat;
        private readonly float yoffset = 560f;
        private readonly float xoffset = 30f;
        private readonly float ypadding = 40f;
        private readonly float xpadding = 35f;
        private readonly float tpadding = 6f;
        public readonly int buildDivFix = -5;  // Literally only used such that the Socks secret code calculation still works
        public int buildDiv = -6;  // Decrement by one everytime a new build is made (TODO: Change such that it can compensate for secret builds or something)
        public readonly Color easyColor = new(0.42f, 0.75f, 0.5f);
        private static readonly string VERSION = "0.3.2.1";
        private readonly Configurable<string> cfgVersion;
        private static string HelloWorld {
            get{
                return Swapper("New in version " + VERSION + ":<LINE><LINE>" +
                "- Fixed Speedster ability not turning off.<LINE>- Fixed calculation for Speedster ability time reduction upon tripping.");
            }
        }

        // Jolly Coop button stuff don't worry about it
        public OpSimpleButton[] jollyEscortBuilds;
        public OpSimpleButton[] jollyEscortEasies;
        //public bool[] jollyEasierState = new bool[4];

        // Arena button stuff
        //public OpSimpleButton[] arenaEscortBuilds;
        //public OpSimpleButton[] arenaEscortEasies;

        public EscOptions(RainWorld rainworld)
        {
            this.rainworld = rainworld;
            this.cfgMeanLizards = this.config.Bind<bool>("cfg_Mean_Lizards", false);
            this.cfgVengefulLizards = this.config.Bind<bool>("cfg_Vengeful_Lizards", false);
            this.cfgHeavyLift = this.config.Bind<float>("cfg_Heavy_Lift", 3f, new ConfigAcceptableRange<float>(0.01f, 1000f));
            this.cfgDKMult = this.config.Bind<float>("cfg_Drop_Kick_Multiplier", 3f, new ConfigAcceptableRange<float>(0.01f, 765f));
            this.cfgElevator = this.config.Bind<bool>("cfg_Elevator", false);
            this.cfgHypable = this.config.Bind<bool>("cfg_Hypable", true);
            this.cfgHypeReq = this.config.Bind<int>("cfg_Hype_Requirement", 3, new ConfigAcceptableRange<int>(0, 6));
            this.cfgHypeRequirement = this.config.Bind<float>("cfg_Hype_Requirement_Value", 0.75f, new ConfigAcceptableRange<float>(-1, 1));
            this.cfgSFX = this.config.Bind<bool>("cfg_SFX", false);
            this.cfgPounce = this.config.Bind<bool>("cfg_Pounce", true);
            this.cfgLongWallJump = this.config.Bind<bool>("cfg_Long_Wall_Jump", false);
            this.cfgDKAnimation = this.config.Bind<bool>("cfg_Drop_Kick_Animation", true);
            //this.cfgBuildNum = this.config.Bind<int>("cfg_Build", 0, new ConfigAcceptableRange<int>(this.buildDiv, 0));
            //this.cfgBuildP1 = this.config.Bind<int>("cfg_Build_P1", 0, new ConfigAcceptableRange<int>(this.buildDiv, 0));
            //this.cfgBuildP2 = this.config.Bind<int>("cfg_Build_P2", 0, new ConfigAcceptableRange<int>(this.buildDiv, 0));
            //this.cfgBuildP3 = this.config.Bind<int>("cfg_Build_P3", 0, new ConfigAcceptableRange<int>(this.buildDiv, 0));
            //this.cfgBuildP4 = this.config.Bind<int>("cfg_Build_P4", 0, new ConfigAcceptableRange<int>(this.buildDiv, 0));
            PlayerCount = Mathf.Max(4, RainWorld.PlayerObjectBodyColors.Length, rainworld.options.controls.Length);
            this.cfgBuild = new Configurable<int>[PlayerCount];  // Make this expandable to more than 4 players by checking how many players are being logged in?
            this.cfgEasy = new Configurable<bool>[PlayerCount];  // This too
            this.cfgBindKeys = new Configurable<KeyCode>[PlayerCount];
            this.cfgCustomBinds = new Configurable<bool>[PlayerCount];
            for (int x = 0; x < PlayerCount; x++){
                this.cfgBuild[x] = this.config.Bind<int>("cfg_Build_Player" + x, 0, new ConfigAcceptableRange<int>(this.buildDiv, 0));
                this.cfgEasy[x] = this.config.Bind<bool>("cfg_Easy_Player" + x, false);
                this.cfgBindKeys[x] = this.config.Bind<KeyCode>("cfg_Custom_Escort_Keybinds_Player" + x, KeyCode.None);
                this.cfgCustomBinds[x] = this.config.Bind<bool>("cfg_Enable_Custom_Escort_Binds_Player" + x, false);
            }
            //this.cfgEasyP1 = this.config.Bind<bool>("cfg_Easy_P1", false);
            //this.cfgEasyP2 = this.config.Bind<bool>("cfg_Easy_P2", false);
            //this.cfgEasyP3 = this.config.Bind<bool>("cfg_Easy_P3", false);
            //this.cfgEasyP4 = this.config.Bind<bool>("cfg_Easy_P4", false);
            //this.cfgCustomP1 = this.config.Bind<bool>("cfg_Custom_P1", false);
            //this.cfgCustomP2 = this.config.Bind<bool>("cfg_Custom_P2", false);
            //this.cfgCustomP3 = this.config.Bind<bool>("cfg_Custom_P3", false);
            //this.cfgCustomP4 = this.config.Bind<bool>("cfg_Custom_P4", false);
            this.cfgDunkin = this.config.Bind<bool>("cfg_Dunkin_Lizards", true);
            this.cfgSpears = this.config.Bind<bool>("cfg_Super_Spear", true);
            this.cfgNoticeHype = this.config.Bind<bool>("cfg_Noticeable_Hype", false);
            this.cfgNoticeEmpower = this.config.Bind<bool>("cfg_Noticeable_Empower", false);
            this.cfgFunnyDeflSlide = this.config.Bind<bool>("cfg_Funny_Deflector_Slide", false);
            this.cfgPoleBounce = this.config.Bind<bool>("cfg_Pole_Bounce", false);
            this.cfgOldSpeedster = this.config.Bind<bool>("cfg_Old_Speedster", false);
            this.cfgOldEscapist = this.config.Bind<bool>("cfg_Old_Escapist", false);
            this.cfgDeveloperMode = this.config.Bind<bool>("cfg_Dev_Log_Mode", false);
            this.cfgDeflecterSharedPool = this.config.Bind<bool>("cfg_Deflector_Shared_Pool", false);
            this.cfgDeveloperMode.OnChange += LongDevLogChange;
            this.cfgSecret = this.config.Bind<int>("cfg_EscSecret", 765, new ConfigAcceptableRange<int>(0, 99999));
            this.cfgSectret = this.config.Bind<bool>("cfg_EscSectret", false);
            this.cfgSectretBuild = this.config.Bind<bool>("cfg_EscSectretBuild", false);
            this.cfgSectretGod = this.config.Bind<bool>("cfg_EscSectretGod", false);
            this.cfgSectretMagic = this.config.Bind<bool>("cfg_EscSectretMagic", false);
            this.cfgEscLaunchH = this.config.Bind<float>("cfg_Escort_Launch_Horizontal", 3f, new ConfigAcceptableRange<float>(0.01f, 50f));
            this.cfgEscLaunchV = this.config.Bind<float>("cfg_Escort_Launch_Vertical", 3f, new ConfigAcceptableRange<float>(0.01f, 50f));
            this.cfgEscLaunchSH = this.config.Bind<float>("cfg_Escort_Launch_Spear", 3f, new ConfigAcceptableRange<float>(0.01f, 50f));
            this.cfgLogImportance = this.config.Bind<int>("cfg_Log_Importance", 0, new ConfigAcceptableRange<int>(-1, 4));
            this.sctTestBuild = this.config.Bind<bool>("sct_Test_Build", false);
            this.cfgSpeedsterGears = this.config.Bind<int>("cfg_Speedster_Gear_Limit", 4, new ConfigAcceptableRange<int>(1, 42));

            
            this.cfgSecret.OnChange += InputSecret;
            this.cfgLogImportance.OnChange += SetLogImportance;
            this.buildEasy = new OpCheckBox[PlayerCount];  // Only the first four are shown. The rest are hidden.
            this.buildPlayer = new OpSliderTick[PlayerCount];
            this.cfgVersion = this.config.Bind<string>("cfg_Escort_Version", VERSION);
            this.hudShowOptions = new()
            {
                new ListItem(Translate("hide"), Translate("Hide"), 0),
                //new ListItem("map", Translate("Show With Map"), 1),
                //new ListItem("relevant", Translate("Show When Relevant"), 2),
                new ListItem(Translate("always"), Translate("Always Show"), 3)
            };
            this.hudLocaOptions = new()
            {
                new ListItem(Translate("botleft"), Translate("Bottom Left"), 0),
                new ListItem(Translate("botmid"), Translate("Bottom Middle"), 1),
                new ListItem(Translate("leftstack"), Translate("Left Stacked"), 2)
            };
            this.cfgShowHud = this.config.Bind<string>("cfg_Show_Hud", hudShowOptions[1].name);
            this.cfgHudLocation = this.config.Bind("cfg_Hud_Location", hudLocaOptions[0].name);
            this.cfgNoMoreFlips = this.config.Bind<bool>("cfg_Shutup_Flips", false);
            this.buildDraggerHelper = config.Bind("escort_builddragger_helper_ignore_this", 0, new ConfigAcceptableRange<int>(1, PlayerCount));
            /*this.buildItems = new()
            {
                new ListItem("default", Translate("Default"), 0),
                new ListItem("brawler", Translate("Brawler"), -1),
                new ListItem("deflector", Translate("Deflector"), -2),
                new ListItem("escapist", Translate("Escapist"), -3),
                new ListItem("railgunner", Translate("Railgunner"), -4),
                new ListItem("speedster", Translate("Speedster"), -5),
                new ListItem("gilded", Translate("Gilded"), -6),
                new ListItem("barbarian", Translate("Barbarian"), -7),
                new ListItem("unstable", Translate("Unstable"), -8)
            };*/
            this.buildItems = new()
            {
                new ListItem("default", Translate("Default"), 0),
                new ListItem("brawler", Translate("Brawler"), -1),
                new ListItem("deflector", Translate("Deflector"), -2),
                new ListItem("escapist", Translate("Escapist"), -3),
                new ListItem("railgunner", Translate("Railgunner"), -4),
                new ListItem("speedster", Translate("Speedster"), -5),
                new ListItem("gilded", Translate("Gilded"), -6)
            };
            this.buildSelectHelper = config.Bind("escort_buildselect_helper_ignore_this", buildItems[0].name);
            this.easySelectHelper = config.Bind("escort_easyselect_helper_ignore_this", false);

            this.bindDraggerHelper = config.Bind("escort_binddragger_helper_ignore_this", 0, new ConfigAcceptableRange<int>(1, PlayerCount));
            this.bindSelectHelper = config.Bind("escort_bindselect_helper", false);
            this.bindKeyHelper = config.Bind("escort_bindkey_helper", KeyCode.None);

            this.cfgAllBuildsGetPup = config.Bind("cfg_Let_All_The_Builds_Have_Slugpups", false);


            // Plugin.ins.L().Christmas(this.cfgSectret.Value);
            // Plugin.ins.L().Easter(this.cfgSectretBuild.Value);
            // Plugin.ins.L().Valentines(this.cfgSectretGod.Value);
        }


        private string SetDefault(string def, string norm = "")
        {
            string text = Translate(" (") + Translate("Default") + Translate("=<DEF>") + (norm != ""? (Translate(", ") + Translate("Vanilla") + Translate("=<NORM>") + Translate(")")) : Translate(")"));
            text = text.Replace("<DEF>", def);
            if (norm != "") text = text.Replace("<NORM>", norm);
            return text;
        }

        public override void Initialize()
        {
            float xo = this.xoffset;
            float yo = this.yoffset;
            float xp = this.xpadding;
            float yp = this.ypadding;
            float tp = this.tpadding;

            tempColor = new(0.5f, 0.5f, 0.55f);
            Color descColor = new(0.53f, 0.48f, 0.59f);

            p1Color = new Color(1f, 1f, 1f);
            p2Color = new Color(1f, 1f, 0.451f);
            p3Color = new Color(1f, 0.451f, 0.451f);
            p4Color = new Color(0.09f, 0.1373f, 0.306f);
            //Color p4Color = RWCustom.Custom.HSL2RGB(0.63055557f, 0.54f, 0.2f);

            Color bShadow = new Color(0.1f, 0.1f, 0.1f);
            Color bDefault = new Color(0.75f, 0.75f, 0.75f);
            Color bBrawler = new Color(0.8f, 0.4f, 0.6f);
            Color bDeflector = new Color(0.69f, 0.55f, 0.9f);
            Color bEscapist = new Color(0.0f, 0.8f, 0.5f);
            Color bRailgunner = new Color(0.5f, 0.85f, 0.78f);
            Color bSpeedster = new Color(0.76f, 0.78f, 0f);
            Color bGilded = new Color(0.796f, 0.549f, 0.27843f);
            Color bUltKill = new Color(0.7f, 0.2f, 0.2f);
            Color bTesting = new Color(0.9f, 0.0f, 0.9f);
            this.buildColors = new Color[]{
                bDefault, bBrawler, bDeflector, bEscapist, bRailgunner, bSpeedster, bGilded
            };
            // I'm so done with this shit, may we never remotely reach 1.5k


            this.secretText = new OpTextBox(this.cfgSecret, new Vector2(xo + (xp * 14f), yo - (yp * 14)), 60)
            {
                description = OptionInterface.Translate("Hmm? What's this?"),
                colorEdge = new Color(0.9294f, 0.898f, 0.98f, 0.55f),
                colorFill = new Color(0.1843f, 0.1843f, 0.1843f, 0.55f),
                colorText = new Color(0.9294f, 0.898f, 0.98f, 0.55f),
                maxLength = 5
            };
            this.secretText.OnValueChanged += InputTheSecret;

            //this.sctTestBuildText = new OpLabel(xo + (xp * 2), yo - (yp * 10.5f) - (tp * 1.3f), Translate("ALPHATESTING") + "[Unstable] {?????}", true){
            //    color = bTesting * 0.7f
            //};
            // This is meaningless since the option isn't actually hidden
            // if (this.sctTestBuild.Value)
            // {
            //     this.sctTestBuildText.Show();
            // }
            // else
            // {
            //     this.sctTestBuildText.Hide();
            // }

            /*
            this.hypableBtn = new OpCheckBox(this.cfgHypable, new Vector2(xo + (xp * 0), yo - (yp * 6) + tp/2)){
                description = OptionInterface.Translate("Enables/disables Escort's Battle-Hype mechanic. (Default=true)")
            };
            this.hypedSlide = new OpSliderTick(this.cfgHypeReq, new Vector2(xo + (xp * 1) + 7f, yo - (yp * 6)), 400 - (int)xp - 7){
                min = 0,
                max = 6,
                description = OptionInterface.Translate("Determines how lenient the Battle-Hype requirements are. (Default=3)"),
            };
            //this.hypableBtn.OnDeactivate += setTheHype;
            //this.hypableBtn.OnReactivate += killTheHype;
            */
            this.hypeableBox = new OpCheckBox(this.cfgHypable, new Vector2(xo + (xp * 0), yo - (yp * 7) + tp / 2))
            {
                description = OptionInterface.Translate("escoptions_hypecheckbox_desc") + SetDefault(cfgHypable.defaultValue)
            };
            this.hypeableBox.OnValueChanged += ToggleDisableHyped;
            this.hypeableTick = new OpSliderTick(this.cfgHypeReq, new Vector2(xo + (xp * 1) + 7f, yo - (yp * 7)), 400 - (int)xp - 7)
            {
                min = 0,
                max = 6,
                description = OptionInterface.Translate("escoptions_hypeslider_desc") + SetDefault(cfgHypeReq.defaultValue)
            };
            this.hypeableTick.OnValueUpdate += RunItThruHyped;
            this.hypeableText = new OpLabel[7];
            for (int i = 0; i < this.hypeableText.Length; i++)
            {
                this.hypeableText[i] = new OpLabel(440f + xp - tp, yo - (yp * 6) - (tp * 2 * i), i switch
                {
                    1 => Translate("escoptions_hypeselect_b"),
                    2 => Translate("escoptions_hypeselect_c"),
                    3 => Translate("escoptions_hypeselect_d"),
                    4 => Translate("escoptions_hypeselect_e"),
                    5 => Translate("escoptions_hypeselect_f"),
                    6 => Translate("escoptions_hypeselect_g"),
                    _ => Translate("escoptions_hypeselect_a")
                })
                {
                    color = this.cfgHypeReq.Value == i && !this.hypeableTick.greyedOut ? Menu.MenuColorEffect.rgbMediumGrey : Menu.MenuColorEffect.rgbDarkGrey
                };
            }

            this.sillySFX = new OpCheckBox(this.cfgSFX, new Vector2(xo + (xp * 0), yo - (yp * 2))){
                colorEdge = tempColor,
                description = OptionInterface.Translate("escoptions_sillysfx_desc") + SetDefault(cfgSFX.defaultValue)
            };

            this.sillySFX.OnValueChanged += TurnNoFlipOnAndOff;

            this.shutUpFlipText = new OpLabel(xo + (xp * 6), yo - (yp * 2) + tp/2, Translate("escoptions_noflipsfx_text")){
                color = cfgSFX.Value? tempColor/2 : tempColor
            };

            this.shutUpFlip = new OpCheckBox(this.cfgNoMoreFlips, new Vector2(xo + (xp * 5), yo - (yp * 2))){
                colorEdge = tempColor,
                greyedOut = !cfgSFX.Value,
                description = OptionInterface.Translate("escoptions_noflipsfx_desc") + SetDefault(cfgNoMoreFlips.defaultValue)
            };

            this.buildDragger = new OpDragger(buildDraggerHelper, new Vector2(xo + (xp * 0), yo - (yp * 14)))
            {
                min = 1,
                max = PlayerCount
            };
            this.buildDragger.OnValueChanged += SelectABuild;
            this.buildSelect = new OpComboBox(buildSelectHelper, new Vector2(xo + (xp * 1), yo - (yp * 14)), 100, buildItems)
            {
                description = Swapper(Translate("escoptions_buildeasy_desc"), Translate("Player"))
            };
            this.buildSelect.OnValueChanged += ChangeTheBuild;
            this.easySelect = new OpCheckBox(easySelectHelper, new Vector2(xo + (xp * 4), yo - (yp * 14)))
            {
                colorEdge = easyColor,
                description = Swapper(Translate("escoptions_buildeasy_desc"), Translate("Player"))
            };
            this.easySelect.OnValueChanged += ChangeTheEasy;
            this.buildManyCats = new OpLabel(xo + (xp * 0), yo - (yp * 13), Translate("escoptions_moreplayerbuilds_text"));
            if (PlayerCount <= 4)
            {
                buildDragger.Hide();
                buildSelect.Hide();
                easySelect.Hide();
                buildManyCats.Hide();
            }

            cfgBindKeysContainer = new OpKeyBinder[PlayerCount];
            cfgCustomBindsContainer = new OpCheckBox[PlayerCount];
            for (int l = 0; l < PlayerCount; l++)
            {
                cfgBindKeysContainer[l] = new OpKeyBinder(cfgBindKeys[l], default, default, false);
                cfgBindKeysContainer[l].Hide();
                cfgCustomBindsContainer[l] = new OpCheckBox(cfgCustomBinds[l], default);
                cfgCustomBindsContainer[l].Hide();
            }


            this.bindDragger = new OpDragger(bindDraggerHelper, xo + (xp * 0), yo - (yp * 7));
            this.bindDragger.OnValueChanged += SelectAPlayer;
            this.bindSelect = new OpCheckBox(bindSelectHelper, new Vector2(xo + (xp * 1), yo - (yp * 7)))
            {
                description = Swapper(Translate("escoptions_custombinds_desc"), Translate("Allow")) + bindDragger.value
            };
            this.bindSelect.OnValueChanged += ToggleCustomKeybind;
            this.bindKey = new OpKeyBinder(bindKeyHelper, new(xo + (xp * 2), yo - (yp * 7)), new(160, 30))
            {
                description = Swapper(Translate("escoptions_custombinds_desc"), Translate("Set")) + bindDragger.value
            };
            this.bindKey.OnValueChanged += SetCustomKeybind;
            this.bindReset = new OpSimpleButton(new(xo + (xp * 7), yo - (yp * 7f)), new(100, 30), Translate("Reset Keybind"))
            {
                description = Swapper(Translate("escoptions_custombinds_desc"), Translate("Reset")) + bindDragger.value
            };
            this.bindReset.OnClick += ResetCustomKeybind;
            this.bindText = new OpLabel(xo + (xp * 10), yo - (yp * 7) + tp/2, Translate("escoptions_custombinds_text"));


            /*
            this.buildP1 = new OpSliderTick(this.cfgBuildP1, new Vector2(xo - (tp * 5), (yo + tp) - (yp * 2.5f) + (yp * buildDiv)), (int)(yp * -buildDiv), true){
                colorLine = p1Color*0.8f,
                colorEdge = p1Color*0.9f,
                min = this.buildDiv,
                max = 0
            };
            //this.buildP1.OnFocusGet += viewBuild;
            this.buildP2 = new OpSliderTick(this.cfgBuildP2, new Vector2(xo - (tp * 1), (yo + tp) - (yp * 2.5f) + (yp * buildDiv)), (int)(yp * -buildDiv), true){
                colorLine = p2Color*0.8f,
                colorEdge = p2Color*0.9f,
                min = this.buildDiv,
                max = 0
            };
            this.buildP3 = new OpSliderTick(this.cfgBuildP3, new Vector2(xo + (tp * 3), (yo + tp) - (yp * 2.5f) + (yp * buildDiv)), (int)(yp * -buildDiv), true){
                colorLine = p3Color*0.9f,
                colorEdge = p3Color,
                min = this.buildDiv,
                max = 0
            };
            this.buildP4 = new OpSliderTick(this.cfgBuildP4, new Vector2(xo + (tp * 7), (yo + tp) - (yp * 2.5f) + (yp * buildDiv)), (int)(yp * -buildDiv), true){
                colorLine = p4Color*2.4f,
                colorEdge = p4Color*2.8f,
                min = this.buildDiv,
                max = 0
            };*/

            //bool catBeat = rainworld.progression.miscProgressionData.redUnlocked;
            saitBeat = rainworld.progression.miscProgressionData.beaten_Saint;
            base.Initialize();
            OpTab mainTab = new(this, Translate("Main"));
            OpTab buildTab = new(this, Translate("Builds"));
            OpTab gimmickTab = new(this, Translate("Gimmicks"));
            OpTab accessibilityTab = new(this, Translate("Accessibility"));
            this.Tabs = new OpTab[]{
                mainTab,
                buildTab,
                gimmickTab,
                accessibilityTab
            };


            this.mainSet = new UIelement[]{
                new OpLabel(xo, yo, Translate("Options"), true),
                new OpLabelLong(new Vector2(xo, yo - (yp * 2)), new Vector2(500f, yp * 2), Swapper(Translate("escoptions_options"))){
                    color = descColor
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 2) + tp/2, Translate("escoptions_meanlizards_text")){
                    color = tempColor
                },
                new OpCheckBox(this.cfgMeanLizards, new Vector2(xo + (xp * 0), yo - (yp * 2))){
                    description = Translate("escoptions_meanlizards_desc") + SetDefault(cfgMeanLizards.defaultValue),
                    colorEdge = tempColor
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 3) + tp/2, Translate("escoptions_vengefullizards_text") + Translate("[Beta]")){
                    color = tempColor
                },
                new OpCheckBox(this.cfgVengefulLizards, new Vector2(xo + (xp * 0), yo - (yp * 3))){
                    description = Translate("escoptions_vengefullizards_desc") + SetDefault(cfgVengefulLizards.defaultValue),
                    colorEdge = tempColor
                },

                new OpLabel(xo + (xp * 3) + 7f, yo - (yp * 4), Translate("escoptions_carry_text")),
                new OpUpdown(this.cfgHeavyLift, new Vector2(xo + (xp * 0), yo - (yp * 4) - tp), 100, 2){
                    description = Translate("escoptions_carry_desc") + SetDefault(cfgHeavyLift.defaultValue, "0.6")
                },
                new OpLabel(xo + (xp * 3) + 7f, yo - (yp * 5), Translate("escoptions_dropkick_text")),
                new OpUpdown(this.cfgDKMult, new Vector2(xo + (xp * 0), yo - (yp * 5) - tp), 100, 2){
                    description = Translate("escoptions_dropkick_desc") + SetDefault(cfgDKMult.defaultValue),
                },

                new OpLabel(xo + (xp * 0) + 7f, yo - (yp * 6) - tp, Translate("escoptions_hype_text")),
                //hypableBtn, hypedSlide,
                this.hypeableBox,
                this.hypeableTick,
                this.hypeableText[0], this.hypeableText[1], this.hypeableText[2], this.hypeableText[3], this.hypeableText[4], this.hypeableText[5], this.hypeableText[6],
                //new OpLabel(440f + xp - 7, yo - (yp * 7), swapper("0=Always on<LINE>1=50% tiredness<LINE>2=66% tiredness<LINE>3=75% tiredness<LINE>4=80% tiredness<LINE>5=87% tiredness<LINE>6=92% tiredness")),

                new OpLabel(xo + (xp * 1), yo - (yp * 8) + tp/2, Translate("escoptions_longwall_text")){
                    color = tempColor
                },
                new OpCheckBox(this.cfgLongWallJump, new Vector2(xo + (xp * 0), yo - (yp * 8))){
                    description = Translate("escoptions_longwall_desc") + SetDefault(cfgLongWallJump.defaultValue),
                    colorEdge = tempColor
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 9) + tp/2, Translate("escoptions_flippounce_text")),
                new OpCheckBox(this.cfgPounce, new Vector2(xo + (xp * 0), yo - (yp * 9))){
                    description = Translate("escoptions_flippounce_desc") + SetDefault(cfgPounce.defaultValue)
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 10) + tp/2, Translate("escoptions_lizgrab_text")),
                new OpCheckBox(this.cfgDunkin, new Vector2(xo + (xp * 0), yo - (yp * 10))){
                    description = Translate("escoptions_lizgrab_desc") + SetDefault(cfgDunkin.defaultValue)
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 11) + tp/2, Translate("escoptions_superspear_text")),
                new OpCheckBox(this.cfgSpears, new Vector2(xo + (xp * 0), yo - (yp * 11))){
                    description = Translate("escoptions_superspear_desc") + SetDefault(cfgSpears.defaultValue)
                },
                new OpLabel(xo + (xp * 1), yo - (yp * 12) + tp/2, Translate("escoptions_pupsforeveryone_text")),
                new OpCheckBox(this.cfgAllBuildsGetPup, new Vector2(xo + (xp * 0), yo - (yp * 12))){
                    description = Translate("escoptions_pupsforeveryone_desc") + SetDefault(cfgAllBuildsGetPup.defaultValue),
                    colorEdge = tempColor
                }
                /*
                new OpLabel(xo + (xp * 3) + 7f, yo - (yp * 12), "Lifting Power Multiplier"),
                new OpUpdown(this.cfgEscLaunchH, new Vector2(xo + (xp * 0), yo - (yp * 12) - tp), 100, 2){
                    description = Translate("Controls how far does Escort get launched horizontally when stunsliding. (Default=3.0)")
                },

                new OpLabel(xo + (xp * 3) + 7f, yo - (yp * 13), "Lifting Power Multiplier"),
                new OpUpdown(this.cfgEscLaunchV, new Vector2(xo + (xp * 0), yo - (yp * 13) - tp), 100, 2){
                    description = Translate("Controls how far does Escort get launched vertically when stunsliding. (Default=3.0)")
                },

                new OpLabel(xo + (xp * 3) + 7f, yo - (yp * 14), "Lifting Power Multiplier"),
                new OpUpdown(this.cfgEscLaunchSH, new Vector2(xo + (xp * 0), yo - (yp * 14) - tp), 100, 2){
                    description = Translate("Controls how far does Escort get launched horizontally when stunsliding caused by a spear throw. (Default=3.0)")
                }*/
            };

            this.buildTitle = new OpLabel[]{
                new OpLabel(xo + (xp * 2), yo - (yp * 2.5f) - (tp * 1.3f), Translate("Default") + " {***__}", true){
                    color = bDefault * 0.7f
                },
                new OpLabel(xo + (xp * 2), yo - (yp * 3.5f) - (tp * 1.3f), Translate("Brawler") + " {*____}", true){
                    color = bBrawler * 0.7f
                },
                new OpLabel(xo + (xp * 2), yo - (yp * 4.5f) - (tp * 1.3f), Translate("Deflector") + " {*****}", true){
                    color = bDeflector * 0.7f
                },
                new OpLabel(xo + (xp * 2), yo - (yp * 5.5f) - (tp * 1.3f), Translate("Escapist") + " {**___}", true){
                    color = bEscapist * 0.7f
                },
                new OpLabel(xo + (xp * 2), yo - (yp * 6.5f) - (tp * 1.3f), Translate("Railgunner") + " {****_}", true){
                    color = bRailgunner * 0.7f
                },
                new OpLabel(xo + (xp * 2), yo - (yp * 7.5f) - (tp * 1.3f), Translate("Speedster") + " {**___}", true){
                    color = bSpeedster * 0.7f
                },
                new OpLabel(xo + (xp * 2), yo - (yp * 8.5f) - (tp * 1.3f), Translate("Gilded") + " {***__}", true){
                    color = bGilded * 0.7f
                },
                /*
                new OpLabel(xo + (xp * 2), yo - (yp * 9.5f) - (tp * 1.3f), Translate("UNAVAILABLE") + "[Barbarian] {?????}", true){
                    color = Color.green
                },*/
                //sctTestBuildText,
            };
            const string buildTextPad = "  ";
            this.buildText = new UIelement[]{
                // The intended way of playing Escort.
                new OpLabel(xo + (xp * 2), yo - (yp * 2.5f) - (tp * 2.1f), buildTextPad + Translate("default_desc")){
                    color = bDefault
                },

                // More powerful and consistent close-combat, but reduced range efficiency
                new OpLabel(xo + (xp * 2), yo - (yp * 3.5f) - (tp * 2.1f), buildTextPad + Translate("brawler_desc")){
                    color = bBrawler
                },

                // Easier to parry, with empowered damage on success!... at the cost of some base stats.
                new OpLabel(xo + (xp * 2), yo - (yp * 4.5f) - (tp * 2.1f), buildTextPad + Translate("deflector_desc")){
                    color = bDeflector
                },

                // -insert new escapist's descriptor
                new OpLabel(xo + (xp * 2), yo - (yp * 5.5f) - (tp * 2.1f), buildTextPad + Translate("escapist_new_desc")){
                    color = bEscapist
                },

                // Force out of grasps, though don't expect to be fighting much...
                // insert code for the old escapist in place of the new escapist in case the old escapist option is checked. Also have a separate thing where the color of the thing changes depending on the option selected.
                //new OpLabel(xo + (xp * 2), yo - (yp * 5.5f) - (tp * 2.1f), buildTextPad + Translate("escapist_desc")){
                //    color = bEscapist
                //},

                // With the aid of <REPLACE>, dual-wield weapons of the same type for extreme results
                new OpLabel(xo + (xp * 2), yo - (yp * 6.5f) - (tp * 2.1f), buildTextPad + Translate("railgunner_desc")){
                    color = bRailgunner
                },

                // Sometimes you just gotta hit the bricks... and sometimes you just gotta go fast.
                new OpLabel(xo + (xp * 2), yo - (yp * 7.5f) - (tp * 2.1f), buildTextPad + Translate("speedster_desc")){
                    color = bSpeedster
                },

                // "Closer to attunement as intended, grasp the powers of <REPLACE>.", catBeat? "the guardians" : "mysterious entities")
                new OpLabel(xo + (xp * 2), yo - (yp * 8.5f) - (tp * 2.1f), buildTextPad + Swapper(Translate("gilded_desc"), saitBeat? Translate("gilded_sub_a") : Translate("gilded_sub_b"))){
                    color = bGilded
                },
            };
            this.buildShadow = new UIelement[this.buildText.Length];
            for (int i = 0; i < this.buildShadow.Length; i++)
            {
                this.buildShadow[i] = new OpLabel(this.buildText[i].PosX - 1f, this.buildText[i].PosY, this.buildText[i].description)
                {
                    color = bShadow
                };
            }
            // Checkbox/buildtick and it's fancy functions
            for (int j = 0; j < PlayerCount; j++)
            {
                this.buildEasy[j] = new OpCheckBox(
                    /*
                    j switch
                    {
                        0 => this.cfgEasyP1,
                        1 => this.cfgEasyP2,
                        2 => this.cfgEasyP3,
                        _ => this.cfgEasyP4
                    }*/
                    this.cfgEasy[j]
                    , new Vector2(xoffset - (tpadding * (5 - 4 * j)) + 3f, yoffset - 3f - (ypadding * 2))
                )
                {
                    description = j switch
                    {
                        0 => Translate("[P1] "),
                        1 => Translate("[P2] "),
                        2 => Translate("[P3] "),
                        _ => Translate("[P4] ")
                    } + Swapper(Translate("escoptions_buildeasy_desc"), j switch {
                        0 => Translate("Player 1"),
                        1 => Translate("Player 2"),
                        2 => Translate("Player 3"),
                        _ => Translate("Player 4")
                    }),
                    colorEdge = easyColor,
                    colorFill = j switch
                    {
                        0 => p1Color * 0.45f,
                        1 => p2Color * 0.45f,
                        2 => p3Color * 0.7f,
                        _ => p4Color
                    },
                };
                // this.buildEasy[j].SetValueBool(this.jollyEasierState[j]);
                // this.buildEasy[j].SetValueBool(
                //     j switch {
                //         0 => this.cfgEasyP1.Value,
                //         1 => this.cfgEasyP2.Value,
                //         2 => this.cfgEasyP3.Value,
                //         _ => this.cfgEasyP4.Value
                //     }
                // );
                // try {
                //     this.buildEasy.SetValue(j switch {
                //         0 => this.cfgEasyP1.Value,
                //         1 => this.cfgEasyP2.Value,
                //         2 => this.cfgEasyP3.Value,
                //         _ => this.cfgEasyP4.Value
                //     }, j);
                // } catch (Exception err){
                //     Ebug(err, "Oh no not my remix menu");
                // }
                    /*
                this.buildPlayer[j] = new OpSliderTick(
                    j switch
                    {
                        0 => this.cfgBuildP1,
                        1 => this.cfgBuildP2,
                        2 => this.cfgBuildP3,
                        _ => this.cfgBuildP4
                    }, new Vector2((xo - (tp * (5 - 4 * j))), (yo + tp) - (yp * 2.5f) + (yp * buildDiv)), (int)(yp * -buildDiv), true
                )
                {
                    value = j switch
                    {
                        0 => ValueConverter.ConvertToString(this.cfgBuildP1.Value, this.cfgBuildP1.settingType),
                        1 => ValueConverter.ConvertToString(this.cfgBuildP2.Value, this.cfgBuildP2.settingType),
                        2 => ValueConverter.ConvertToString(this.cfgBuildP3.Value, this.cfgBuildP3.settingType),
                        _ => ValueConverter.ConvertToString(this.cfgBuildP4.Value, this.cfgBuildP4.settingType)
                    },
                };
                    */
                this.buildPlayer[j] = new OpSliderTick(this.cfgBuild[j], new Vector2((xo - (tp * (5 - 4 * j))), (yo + tp) - (yp * 2.5f) + (yp * buildDiv)), (int)(yp * -buildDiv), true)
                {
                    description = j switch
                    {
                        0 => Translate("[P1] "),
                        1 => Translate("[P2] "),
                        2 => Translate("[P3] "),
                        _ => Translate("[P4] ")
                    } + Swapper(Translate("escoptions_build_desc"), j switch {
                        0 => Translate("Player 1"),
                        1 => Translate("Player 2"),
                        2 => Translate("Player 3"),
                        _ => Translate("Player 4")
                    }),
                };
                (this.buildPlayer[j].colorLine, this.buildPlayer[j].colorEdge) = j switch
                {
                    0 => (p1Color * 0.8f, p1Color * 0.9f),
                    1 => (p2Color * 0.8f, p2Color * 0.9f),
                    2 => (p3Color * 0.9f, p3Color),
                    _ => (p4Color * 2.4f, p4Color * 2.8f)
                };
                if (j > 3)
                {
                    buildEasy[j].Hide();
                    buildPlayer[j].Hide();
                }

                this.buildEasy[j].OnValueChanged += (UIconfig config, string value, string oldValue) =>
                {
                    int target = -1;
                    for (int s = 0; s < 4; s++)
                    {
                        if (this.buildEasy[s].cfgEntry.BoundUIconfig == config)
                        {
                            target = s;
                            break;
                        }
                    }
                    if (target == -1)
                    {
                        Ebug("Config index not found!");
                        return;
                    }
                    this.buildPlayer[target].colorFill = value == "true" ? easyColor * 0.5f : Menu.MenuColorEffect.rgbBlack;
                    if (ValueConverter.ConvertToValue<int>(buildDragger.value) - 1 == target)
                    {
                        easySelect.value = value;
                    }
                };
                this.buildPlayer[j].OnValueChanged += (UIconfig config, string value, string oldValue) =>
                {
                    int target = -1;
                    for (int s = 0; s < 4; s++)
                    {
                        if (this.buildPlayer[s].cfgEntry.BoundUIconfig == config)
                        {
                            target = s;
                            break;
                        }
                    }
                    if (target == -1)
                    {
                        Ebug("Config index not found!");
                        return;
                    }
                    if (ValueConverter.ConvertToValue<int>(buildDragger.value) - 1 == target)
                    {
                        buildSelect.value = buildItems[buildItems.FindIndex(x => x.value == ValueConverter.ConvertToValue<int>(value))].name;
                    }
                };
                /*
                this.buildPlayer[j].OnValueUpdate += (UIconfig config, string value, string oldValue) =>
                {  // Only starts applying once the slider is moved
                    int target = -1;
                    for (int s = 0; s < this.buildPlayer.Length; s++)
                    {
                        if (this.buildPlayer[s].cfgEntry.BoundUIconfig == config)
                        {
                            target = s;
                            break;
                        }
                    }
                    if (target == -1)
                    {
                        Ebug("Config index not found!");
                        return;
                    }
                    if (this.buildPlayer.Length < 4)
                    {
                        Ebug("Index error?! " + this.buildPlayer.Length + "  Target: " + target);
                        return;
                    }
                    DoTheBuildColorThing(target, value);
                };

                this.buildPlayer[j].OnHeld += (bool isHeld) =>
                {  // Only applies on selecting/deselecting the slider
                    if (!isHeld)
                    {
                        for (int m = 0; m < this.buildTitle.Length; m++)
                        {
                            (this.buildTitle[m] as OpLabel).color = this.buildColors[m] * 0.75f;
                            (this.buildText[m] as OpLabel).color = this.buildColors[m];
                        }
                    }
                    else
                    {
                        int target = -1;
                        for (int s = 0; s < this.buildPlayer.Length; s++)
                        {
                            if (this.buildPlayer[s].held)
                            {
                                target = s;
                                break;
                            }
                        }
                        if (target == -1)
                        {
                            Ebug("Config index not found!");
                            return;
                        }
                        if (this.buildPlayer.Length < 4)
                        {
                            Ebug("Index error?! " + this.buildPlayer.Length + "  Target: " + target);
                            return;
                        }
                        DoTheBuildColorThing(target, this.buildPlayer[target].value);
                    }
                };
                */
            }
            this.buildSet = new UIelement[]{
                new OpLabel(xo, yo, Translate("Builds"), true),
                new OpLabelLong(new Vector2(xo, yo - (yp * 2)), new Vector2(500f, yp * 2), Swapper(Translate("escoptions_builds"))){
                    color = descColor
                },

                new OpLabel(xo + (xp * 2) + (tp * 1.5f), yo - 3f - (yp * 2) + tp/2, Translate("escoptions_buildeasy_text")){
                    color = easyColor
                },
                // this.buildEasy[0],
                // this.buildEasy[1],
                // this.buildEasy[2],
                // this.buildEasy[3],

                new OpLabel(xo - (tp * 3.8f), yo + 3f - (yp * 1.5f), Translate("(1)   (2)   (3)   (4) <-PLAYER #")){
                    color = new Color(0.5f, 0.5f, 0.5f)
                },

                // // Sliders
                // this.buildPlayer[0],
                // this.buildPlayer[1],
                // this.buildPlayer[2],
                // this.buildPlayer[3],
                this.buildManyCats,
                this.buildDragger,
                this.buildSelect,
                this.easySelect
            };
            this.gimmickSet = new UIelement[]{
                new OpLabel(xo, yo, Translate("Gimmicks"), true),
                new OpLabelLong(new Vector2(xo, yo - (yp * 2)), new Vector2(500f, yp * 2), Translate("escoptions_gimmicks")){
                    color = descColor
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 2) + tp/2, Translate("escoptions_sillysfx_text")){
                    color = tempColor
                },
                sillySFX,

                shutUpFlipText,
                shutUpFlip,

                new OpLabel(xo + (xp * 1), yo - (yp * 3) + tp/2, Translate("escoptions_elevator_text")){
                    color = tempColor
                },
                new OpCheckBox(this.cfgElevator, new Vector2(xo + (xp * 0), yo - (yp * 3))){
                    colorEdge = tempColor,
                    description = OptionInterface.Translate("escoptions_elevator_desc") + SetDefault(cfgElevator.defaultValue),
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 4) + tp/2, Translate("escoptions_propdk_text")),
                new OpCheckBox(this.cfgDKAnimation, new Vector2(xo + (xp * 0), yo - (yp * 4))){
                    description = OptionInterface.Translate("escoptions_propdk_desc") + SetDefault(cfgDKAnimation.defaultValue),
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 5) + tp/2, Swapper(Translate("escoptions_deflslide_text"), Translate("Deflector"))){
                    color = tempColor
                },
                new OpCheckBox(this.cfgFunnyDeflSlide, new Vector2(xo + (xp * 0), yo - (yp * 5))){
                    colorEdge = tempColor,
                    description = Swapper(Translate("escoptions_deflslide_desc")) + SetDefault(cfgFunnyDeflSlide.defaultValue),
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 6) + tp/2, Translate("escoptions_poletech_text")){
                    color = tempColor
                },
                new OpCheckBox(this.cfgPoleBounce, new Vector2(xo + (xp * 0), yo - (yp * 6))){
                    colorEdge = tempColor,
                    description = Swapper(Translate("escoptions_poletech_desc"), Translate("Rivulet")) + SetDefault(cfgPoleBounce.defaultValue),
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 7) + tp/2, Swapper(Translate("escoptions_oldspeed_text"))){
                    color = tempColor
                },
                new OpCheckBox(this.cfgOldSpeedster, new Vector2(xo + (xp * 0), yo - (yp * 7))){
                    colorEdge = tempColor,
                    description = Swapper(Translate("escoptions_oldspeed_desc")) + SetDefault(cfgOldSpeedster.defaultValue),
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 8) + tp/2, Swapper(Translate("escoptions_deflshared_text"))){
                    color = tempColor
                },
                new OpCheckBox(this.cfgDeflecterSharedPool, new Vector2(xo + (xp * 0), yo - (yp * 8))){
                    colorEdge = tempColor,
                    description = Swapper(Translate("escoptions_deflshared_desc")) + SetDefault(cfgDeflecterSharedPool.defaultValue),
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 9) + tp/2, Swapper(Translate("escoptions_oldescape_text"))){
                    color = tempColor
                },
                new OpCheckBox(this.cfgOldEscapist, new Vector2(xo + (xp * 0), yo - (yp * 9))){
                    colorEdge = tempColor,
                    description = Swapper(Translate("escoptions_oldescape_desc")) + SetDefault(cfgOldEscapist.defaultValue),
                },

                new OpLabel(xo + (xp * 3) + 7f, yo - (yp * 10), Translate("Speedster Gear Limit")),
                new OpUpdown(this.cfgSpeedsterGears, new Vector2(xo + (xp * 0), yo - (yp * 10) - tp), 100){
                    description = Translate("Sets the gear limit for the Speedster build. Handle with care!") + SetDefault(cfgSpeedsterGears.defaultValue)
                },

                secretText
            };
            this.accessibleSet = new UIelement[]{
                new OpLabel(xo, yo, Translate("Accessibility"), true),
                new OpLabelLong(new Vector2(xo, yo - (yp * 2)), new Vector2(500f, yp * 2), Translate("escoptions_accessibility")){
                    color = descColor
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 2) + tp/2, Translate("escoptions_hypeindicator_text")){
                    color = tempColor
                },
                new OpCheckBox(this.cfgNoticeHype, new Vector2(xo + (xp * 0), yo - (yp * 2))){
                    colorEdge = tempColor,
                    description = OptionInterface.Translate("escoptions_hypeindicator_desc") + SetDefault(cfgNoticeHype.defaultValue)
                },

                new OpLabel(xo + (xp * 1), yo - (yp * 3) + tp/2, Swapper(Translate("escoptions_deflaltvfx_text"))){
                    color = tempColor
                },
                new OpCheckBox(this.cfgNoticeEmpower, new Vector2(xo + (xp * 0), yo - (yp * 3))){
                    colorEdge = tempColor,
                    description = Swapper(Translate("escoptions_deflaltvfx_desc")) + SetDefault(cfgNoticeEmpower.defaultValue)
                },

                // new OpLabel(xo + (xp * 1), yo - (yp * 4) + tp/2, "Keep Track of All Methods (for modders)"){
                //     color = tempColor * 0.75f
                // },
                // new OpCheckBox(this.cfgDeveloperMode, new Vector2(xo + (xp * 0), yo - (yp * 4))){
                //     colorEdge = tempColor * 0.75f,
                //     description = OptionInterface.Translate("For other modders: Also keep track of frequently updated methods (May cause a bit of lag). (Default=false)"),
                //     greyedOut = true
                // },

                new OpLabel(xo + (xp * 8) + 7f, yo - (yp * 4) + tp, Translate("escoptions_devlog_text")),
                new OpSliderTick(this.cfgLogImportance, new Vector2(xo + (xp * 0) + 7f, yo - (yp * 4)), 300 - (int)xp - 7)
                {
                    min = -1,
                    max = 4,
                    description = OptionInterface.Translate("escoptions_devlog_desc") + SetDefault(cfgLogImportance.defaultValue),
                },
                new OpLabel(xo + (xp * 5) + 7f, yo - (yp * 6), Translate("escoptions_hudloc_text") + Translate("[Beta]")),
                new OpComboBox(this.cfgHudLocation, new Vector2(xo + (xp * 0), yo - (yp * 6) - tp), 160, hudLocaOptions){
                    description = Translate("escoptions_hudloc_desc") + SetDefault(cfgHudLocation.defaultValue)
                },

                new OpLabel(xo + (xp * 5) + 7f, yo - (yp * 5), Translate("escoptions_hudshow_text") + Translate("[Beta]")),
                new OpComboBox(this.cfgShowHud, new Vector2(xo + (xp * 0), yo - (yp * 5) - tp), 160, hudShowOptions){
                    description = Translate("escoptions_hudshow_desc") + SetDefault(cfgShowHud.defaultValue)
                },

                bindDragger,
                bindKey,
                bindSelect,
                bindReset,
                bindText

            };
            mainTab.AddItems(this.mainSet);
            buildTab.AddItems(this.buildSet);
            buildTab.AddItems(buildEasy);
            buildTab.AddItems(buildPlayer);
            buildTab.AddItems(this.buildTitle);
            buildTab.AddItems(this.buildShadow);
            buildTab.AddItems(this.buildText);
            gimmickTab.AddItems(this.gimmickSet);
            accessibilityTab.AddItems(this.accessibleSet);
            accessibilityTab.AddItems(cfgBindKeysContainer);
            accessibilityTab.AddItems(cfgCustomBindsContainer);
            if (cfgVersion.Value != VERSION){
                ConfigConnector.CreateDialogBoxNotify(HelloWorld);
                cfgVersion.Value = VERSION;
                //this._SaveConfigFile();
            }
        }

        private void ResetCustomKeybind(UIfocusable trigger)
        {
            int playerNumber = ValueConverter.ConvertToValue<int>(bindDragger.value) - 1;
            string oldValueThing = cfgBindKeysContainer[playerNumber].value;
            cfgBindKeysContainer[playerNumber].value = ValueConverter.ConvertToString(KeyCode.None);
            bindKey.value = ValueConverter.ConvertToString(KeyCode.None);
            Ebug($"Reset bind from {oldValueThing} to {cfgBindKeysContainer[playerNumber].value}", 1, true);
        }

        private void SetCustomKeybind(UIconfig config, string value, string oldValue)
        {
            int playerNumber = ValueConverter.ConvertToValue<int>(bindDragger.value) - 1;
            Ebug($"Value is {value}", 1, true);
            string oldValueThing = cfgBindKeysContainer[playerNumber].value;
            cfgBindKeysContainer[playerNumber].value = value;
            Ebug($"Set bind from {oldValueThing} to {cfgBindKeysContainer[playerNumber].value}", 1, true);
        }

        private void ToggleCustomKeybind(UIconfig config, string value, string oldValue)
        {
            int playerNumber = ValueConverter.ConvertToValue<int>(bindDragger.value) - 1;
            bool oldValueThing = cfgCustomBindsContainer[playerNumber].GetValueBool();
            cfgCustomBindsContainer[playerNumber].value = value;
            if (cfgCustomBindsContainer[playerNumber].GetValueBool())
            {
                bindKey.greyedOut = false;
                bindReset.greyedOut = false;
            }
            else
            {
                bindKey.greyedOut = true;
                bindReset.greyedOut = true;
            }
            Ebug($"Set custom bind from {oldValueThing} to {cfgCustomBindsContainer[playerNumber].GetValueBool()}", 1, true);
        }

        private void SelectAPlayer(UIconfig config, string value, string oldValue)
        {
            int playerNumber = ValueConverter.ConvertToValue<int>(value) - 1;
            bindSelect.value = cfgCustomBindsContainer[playerNumber].value;
            bindSelect.description = Swapper(Translate("escoptions_custombinds_desc"), Translate("Allow")) + value;
            bindKey.value = ValueConverter.ConvertToString(ValueConverter.ConvertToValue<KeyCode>(cfgBindKeysContainer[playerNumber].value));
            bindKey.description = Swapper(Translate("escoptions_custombinds_desc"), Translate("Set")) + value;
            bindKey.greyedOut = !cfgCustomBindsContainer[playerNumber].GetValueBool();
            bindReset.description = Swapper(Translate("escoptions_custombinds_desc"), Translate("Reset")) + value;
            bindReset.greyedOut = !cfgCustomBindsContainer[playerNumber].GetValueBool();
            Ebug($"Settings focus from {oldValue} to {value}", 1, true);
        }

        private void ChangeTheEasy(UIconfig config, string value, string oldValue)
        {
            int playerNumber = ValueConverter.ConvertToValue<int>(buildDragger.value) - 1;
            bool oldValueThing = buildEasy[playerNumber].GetValueBool();
            buildEasy[playerNumber].SetValueBool(ValueConverter.ConvertToValue<bool>(value));
            Ebug($"Set build from {oldValueThing} to {buildEasy[playerNumber].GetValueBool()}", 1, true);
        }

        private void ChangeTheBuild(UIconfig config, string value, string oldValue)
        {
            int playerNumber = ValueConverter.ConvertToValue<int>(buildDragger.value) - 1;
            int oldValueThing = buildPlayer[playerNumber].GetValueInt();
            buildPlayer[playerNumber].SetValueInt(buildItems[buildItems.FindIndex(x => x.name == value)].value);
            Ebug($"Set build from {oldValueThing} to {buildPlayer[playerNumber].GetValueInt()}", 1, true);
        }

        private void SelectABuild(UIconfig config, string value, string oldValue)
        {
            int playerNumber = ValueConverter.ConvertToValue<int>(value) - 1;
            buildSelect.value = buildItems[buildItems.FindIndex(x => x.value == buildPlayer[playerNumber].GetValueInt())].name;
            easySelect.value = buildEasy[playerNumber].value;
            Ebug($"Settings focus from {oldValue} to {value}", 1, true);
        }

        public override void Update()
        {
            base.Update();
            if (cfgVersion.Value != VERSION){
                cfgVersion.Value = VERSION;
                this._SaveConfigFile();
            }
        }

        private void LongDevLogChange()
        {
            if (this.cfgDeveloperMode.Value)
            {
                Plugin.ins.L().TurnOnLog();
            }
            else
            {
                Plugin.ins.L().TurnOffLog();
            }
        }
        /*
                private void inputSecret()
                {
                    int num = (int)this.yoffset * (int)this.tpadding - ((int)this.xoffset / 2) * (int)this.ypadding + ((int)this.tpadding - 1) * ((int)this.xoffset + (int)this.xpadding) + 33;
                    string[] insult = new string[1];
                    Action[] doThing = new Action[1]{
                        makeSomeNoiseEsconfig
                    };
                    insult[0] = "Ur not my mum.";
                    switch(UnityEngine.Random.Range(0, 5)){
                        case 1: insult[0] = "F#@k off."; break;
                        case 2: insult[0] = "Skill issue."; break;
                        case 3: insult[0] = "I don't care."; break;
                        case 4: insult[0] = "Shut the f$&k up."; break;
                    }
                    if (this.cfgSecret.Value == num){
                        if (!this.cfgSectret.Value){
                            this.cfgSectret.Value = true;
                            ConfigConnector.CreateDialogBoxMultibutton(
                                swapper(
                                    "     ...though never intent...     <LINE> ...the pup escapes containment... <LINE>  ...careful out there, yeah?...   "
                                ), insult, doThing
                            );
                        }
                        Plugin.ins.L().christmas(this.cfgSectret.Value);
                    }
                    else {
                        this.cfgSectret.Value = false;
                        Plugin.ins.L().christmas();
                        try{
                            if (Plugin.Esconfig_SFX_Sectret != null){
                                ConfigContainer.PlaySound(Plugin.Esconfig_SFX_Sectret);
                            }
                        } catch (Exception err){
                            Debug.LogError("Couldn't play sound!");
                            Debug.LogException(err);
                        }
                    }
                }
        */
        private void InputTheSecret(UIconfig config, string value, string oldValue)
        {
            ResultsBaby(value);
        }

        private void InputSecret()
        {
            ResultsBaby(ValueConverter.ConvertToString(this.cfgSecret.Value, this.cfgSecret.settingType));
        }

        private void ResultsBaby(string value = "")
        {
            int num = (int)this.yoffset * (int)this.tpadding - (int)this.xoffset / 2 * (int)this.ypadding + ((int)this.tpadding - 1) * ((int)this.xoffset + (int)this.xpadding) + 33;
            int nu2 = 1500; int nu3 = 87769; int nu4 = 602; int nu5 = 1984;
            string[] insult = new string[1];
            Action[] doThing = new Action[1]{
                MakeSomeNoiseEsconfig
            };
            insult[0] = Translate("escoptions_insult_a");
            switch (UnityEngine.Random.Range(0, 5))
            {
                case 1: insult[0] = Translate("escoptions_insult_b"); break;
                case 2: insult[0] = Translate("escoptions_insult_c"); break;
                case 3: insult[0] = Translate("escoptions_insult_d"); break;
                case 4: insult[0] = Translate("escoptions_insult_e"); break;
            }
            if (value == num.ToString())
            {
                if (!this.cfgSectret.Value)
                {
                    this.cfgSectret.Value = true;
                    ConfigConnector.CreateDialogBoxMultibutton(
                        Swapper(
                            Translate("escoptions_sectret_spuphaiku")
                        ), insult, doThing
                    );
                }
                Plugin.ins.L().Christmas(this.cfgSectret.Value);
                Ebug("Set secret 1");
            }
            else if (value == nu2.ToString())
            {
                if (!this.cfgSectretBuild.Value)
                {
                    this.cfgSectretBuild.Value = true;
                    ConfigConnector.CreateDialogBoxMultibutton(
                        Swapper(
                            Translate("escoptions_sectret_gilded"), saitBeat? Translate("Rubicon") : Translate("escoptions_sectret_subgild")
                        ), insult, doThing
                    );
                }
                Plugin.ins.L().Easter(this.cfgSectretBuild.Value);
                Ebug("Set secret 2");
            }
            else if (value == nu3.ToString())
            {
                if (!this.cfgSectretGod.Value)
                {
                    this.cfgSectretGod.Value = true;
                    ConfigConnector.CreateDialogBoxMultibutton(
                        Translate(
                            Translate("escoptions_sectret_invincible")
                        ), insult, doThing
                    );
                }
                Plugin.ins.L().Valentines(this.cfgSectretGod.Value);
                Ebug("Set secret 3");
            }
            else if (value == nu4.ToString())
            {
                if (!this.cfgSectretMagic.Value)
                {
                    this.cfgSectretMagic.Value = true;
                    ConfigConnector.CreateDialogBoxMultibutton(
                        Translate(
                            Translate("escoptions_sectret_magic")
                        ), insult, doThing
                    );
                }
                Plugin.ins.L().NewYears(this.cfgSectretMagic.Value);
                Ebug("Set secret 4");
            }
            else if (value == nu5.ToString())
            {
                if (!this.sctTestBuild.Value)
                {
                    this.sctTestBuild.Value = true;
                    ConfigConnector.CreateDialogBoxMultibutton(
                        Translate("Congrats! You have the access code (that you definitely got from the developer) and can now test the lastest upcoming build!"), insult, doThing
                    );
                }
                Ebug("Set secret build testing mode");
            }
            else
            {
                this.cfgSectret.Value = false;
                this.cfgSectretBuild.Value = false;
                this.cfgSectretGod.Value = false;
                this.cfgSectretMagic.Value = false;
                this.sctTestBuild.Value = false;
                Plugin.ins.L().Holiday();
                try
                {
                    if (rainworld.processManager.currentMainLoop is Menu.ModdingMenu && Plugin.Esconfig_SFX_Sectret != null)
                    {
                        ConfigContainer.PlaySound(Plugin.Esconfig_SFX_Sectret);
                    }
                }
                catch (Exception err)
                {
                    Debug.LogError("Couldn't play sound!");
                    Debug.LogException(err);
                }
                Ebug("No More Secrets");
            }        
        }

        private void MakeSomeNoiseEsconfig()
        {
            if (rainworld.processManager.currentMainLoop is Menu.ModdingMenu && SoundID.MENU_Next_Slugcat != null){
                ConfigContainer.PlaySound(SoundID.MENU_Next_Slugcat, 0, 1, 0.6f);
            }
        }

        private void ToggleDisableHyped(UIconfig config, string value, string oldValue)
        {
            if (value == "true")
            {
                int.TryParse(this.hypeableTick.value, out int i);
                this.hypeableTick.greyedOut = false;
                for (int j = 0; j < this.hypeableText.Length; j++)
                {
                    if (i == j)
                    {
                        this.hypeableText[j].color = Menu.MenuColorEffect.rgbMediumGrey;
                    }
                    else
                    {
                        this.hypeableText[j].color = Menu.MenuColorEffect.rgbDarkGrey;
                    }
                }
            }
            else
            {
                this.hypeableTick.greyedOut = true;
                foreach (OpLabel l in this.hypeableText)
                {
                    l.color = Menu.MenuColorEffect.rgbVeryDarkGrey;
                }
            }
        }


        private void TurnNoFlipOnAndOff(UIconfig config, string value, string oldValue)
        {
            if (value == "true")
            {
                this.shutUpFlip.greyedOut = false;
                this.shutUpFlipText.color = tempColor;
            }
            else
            {
                this.shutUpFlip.greyedOut = true;
                this.shutUpFlipText.color = tempColor / 2;
            }
        }



        private void RunItThruHyped(UIconfig config, string value, string oldValue)
        {
            int.TryParse(value, out int n);
            int.TryParse(oldValue, out int o);
            this.hypeableText[n].color = Menu.MenuColorEffect.rgbMediumGrey;
            this.cfgHypeRequirement.Value = n switch {
                0 => -1f,
                1 => 0.5f,
                2 => 0.66f,
                3 => 0.75f,
                4 => 0.8f,
                5 => 0.87f,
                6 => 0.92f,
                _ => 1f
            };
            this.hypeableText[o].color = Menu.MenuColorEffect.rgbDarkGrey;
        }

        private void DoTheBuildColorThing(int index, string value)
        {
            if (!int.TryParse(value, out int r))
            {
                Ebug("Couldn't get value of Player Build!", 0);
                return;
            }
            try
            {
                for (int k = 0; k < this.buildTitle.Length; k++)
                {
                    if (k == -r)
                    {
                        (this.buildTitle[k] as OpLabel).color = index switch
                        {
                            0 => this.buildColors[k] * (this.p1Color * 0.8f),
                            1 => this.buildColors[k] * (this.p2Color * 0.8f),
                            2 => this.buildColors[k] * (this.p3Color * 0.9f),
                            _ => this.buildColors[k] * (this.p4Color * 2.4f)
                        };
                        (this.buildText[k] as OpLabel).color = this.buildColors[k];
                    }
                    else
                    {
                        (this.buildTitle[k] as OpLabel).color = Menu.MenuColorEffect.rgbDarkGrey;
                        (this.buildText[k] as OpLabel).color = Menu.MenuColorEffect.rgbMediumGrey;
                    }
                }
            }
            catch (Exception err)
            {
                Ebug(err, "Error on Focus Gain");
            }
        }

        private void SetLogImportance()
        {
            logImportance = cfgLogImportance.Value;
        }
    }
}
