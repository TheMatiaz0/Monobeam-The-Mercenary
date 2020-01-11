using System;
using TheMatiaz0_MonobeamTheMercenary.Input;
using TheMatiaz0_MonobeamTheMercenary.Progression;
using static TheMatiaz0_MonobeamTheMercenary.Translation.TranslationSystem;

namespace TheMatiaz0_MonobeamTheMercenary.NPC
{
    public class StrangerFirstNPC : TalkableNPC
    {
        public StrangerFirstNPC() : base
        (
            name: DialogueFromFile.Stranger.Name,
            mainColor: ConsoleColor.DarkMagenta,
            frequency: 370,
            charisma: 10
        )
        { }

        private static bool TalkedBefore = false;

        private void FirstDialogue ()
        {
            Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(0), this);

            Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(1), Program.Narrator);

            Dialogue.Dialogue.PrintInputText(DialogueFromFile.Stranger.ChangeDialogue(2));

            InitializeCharacter();

            void InitializeCharacter ()
            {
                // Get the name by Polish language (female names always ends at 'a'):
                if (OptionsManager.Language == "Polish" && !string.IsNullOrWhiteSpace(Program.TempVariable))
                {
                    if (Program.TempVariable[Program.TempVariable.Length - 1] != 'a')
                    {
                        SetGender(Gender.Male);
                        Dialogue.Dialogue.PrintOutputText($"{Program.TempVariable}. {DialogueFromFile.Stranger.ChangeDialogue(3, 0)}", Program.Character);
                    }

                    else
                    {
                        SetGender(Gender.Female);
                        Dialogue.Dialogue.PrintOutputText($"{Program.TempVariable}. {DialogueFromFile.Stranger.ChangeDialogue(3, 1)}", Program.Character);
                    }
                }

                // Get the name by English language, choose by selecting Gender:
                if (OptionsManager.Language != "Polish")
                {
                    ControlEvent.SelectOptions
                    (new Option(DialogueFromFile.Stranger.ChangeDialogue(4, 0), () => SetGender(Gender.Male)),
                    new Option(DialogueFromFile.Stranger.ChangeDialogue(4, 1), () => SetGender(Gender.Female)));
                }

                void SetGender(Gender gender)
                {
                    Program.Character = new Warrior(Program.TempVariable, gender);
                }
            }    

            if (string.IsNullOrWhiteSpace(Program.TempVariable))
            {
                for (int i = 0; i < 4; i++)
                {
                    Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(7, i), this);
                }

                Dialogue.Dialogue.PrintInputText(DialogueFromFile.Stranger.ChangeDialogue(2));

                if (string.IsNullOrWhiteSpace(Program.TempVariable))
                {
                    for (int i = 4; i < 12; i++)
                    {
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(7, i), this, false);
                    }

                    Console.Clear();

                    Ending.Error("0x0000001a");
                }

                else
                {
                    InitializeCharacter();
                }
            }

            if (Program.Character.Gender == Gender.Male)
            {
                Dialogue.Dialogue.PrintOutputText($"{DialogueFromFile.Stranger.ChangeDialogue(5, 0)} {Program.Character.Name}{DialogueFromFile.Stranger.ChangeDialogue(5, 1)}", this);

                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(5, 2), this);
            }

            else if (Program.Character.Gender == Gender.Female)
            {
                Dialogue.Dialogue.PrintOutputText($"{DialogueFromFile.Stranger.ChangeDialogue(6, 0)} {Program.Character.Name}{DialogueFromFile.Stranger.ChangeDialogue(6, 1)}", this);

                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(6, 2), this);
            }

            Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(8), Program.Character);
            Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(9), this);
            Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(10), Program.Character);
            Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(11), this);

            Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(12), Program.Character);

            ControlEvent.SelectOptions
            (new Option(DialogueFromFile.Stranger.ChangeDialogue(13, 0), () => SetCharacterClass("Warrior")),
            new Option(DialogueFromFile.Stranger.ChangeDialogue(13, 1), () => SetCharacterClass("Mage")),
            new Option(DialogueFromFile.Stranger.ChangeDialogue(13, 2), () => SetCharacterClass("Bard")),
            new Option(DialogueFromFile.Stranger.ChangeDialogue(13, 3), () => SetCharacterClass("Lucky")));

            void SetCharacterClass(string characterClass)
            {
                switch (characterClass)
                {
                    case "Warrior":
                        Program.Character = new Warrior(Program.Character.Name, Program.Character.Gender);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(14), this);
                        break;

                    case "Mage":
                        Program.Character = new Mage(Program.Character.Name, Program.Character.Gender);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(15), this);
                        break;

                    case "Bard":
                        Program.Character = new Bard(Program.Character.Name, Program.Character.Gender);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(16, 0), this);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(16, 1), this);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(16, 2), Program.Narrator);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(16, 3), this);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(16, 4), Program.Narrator);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(16, 5), this);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(16, 6), Program.Narrator);
                        break;

                    case "Lucky":
                        Program.Character = new Lucky(Program.Character.Name, Program.Character.Gender);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(17, 0), this);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(17, 1), this);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(17, 2), Program.Character);
                        Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(17, 3), this);
                        break;
                }

                if (Program.DebugMode == false)
                {
                    Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(18, 0), Program.Character);
                    Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(18, 1), this);
                    Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(18, 2), this);

                    ControlEvent.SelectOptions
                    (new Option(DialogueFromFile.Stranger.ChangeDialogue(19, 0), () => SetOutcome(1)),
                    new Option(DialogueFromFile.Stranger.ChangeDialogue(19, 1), () => SetOutcome(2)),
                    new Option(DialogueFromFile.Stranger.ChangeDialogue(19, 2), () => SetOutcome(3)));

                    void SetOutcome(int outcome)
                    {
                        switch (outcome)
                        {
                            case 1:
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(20, 0), Program.Character);
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(20, 1), this);
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(20, 2), Program.Character);
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(20, 3), Program.Narrator);
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(20, 4), this);
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(20, 5), Program.Character);
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(20, 6), this);
                                break;

                            case 2:
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(21, 0), Program.Character);
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(21, 1), this);
                                break;

                            case 3:
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(23), Program.Character);
                                NPCDeath();
                                break;
                        }
                    }

                    void NPCDeath()
                    {
                        if (Program.Character.Gender == Gender.Male)
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(24, i), Program.Narrator);
                            }
                        }

                        else
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Stranger.ChangeDialogue(25, i), Program.Narrator);
                            }
                        }
                    }
                }
                
            }

            TalkedBefore = true;
        }

        public override void Talk()
        {
            if (!TalkedBefore)
            {
                FirstDialogue();
            }
        }
    }
}
