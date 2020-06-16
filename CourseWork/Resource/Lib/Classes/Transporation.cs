namespace CourseWork.Resource.Lib.Classes
{
    class Transporation
    {
        private string[] chords = { "A", "B♭", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
        private string ChordDefine(int index, string text)
        {
            text += " ";
            string chord = "";
            if ((text[index + 1].ToString() == "#") || (text[index + 1].ToString() == "♭"))
            {
                chord = text[index].ToString() + text[index + 1].ToString();
            }
            else
            {
                chord = text[index].ToString();
            }

            foreach (var item in chords)
            {
                if (item == chord)
                {
                    return chord;
                }
            }
            return "Undefine";
        }

        public string TransporateUp(string text)
        {
            string transporatedText = "";
            for (int i = 0; i < text.Length; i++)
            {
                bool check = false;
                string leter = text[i].ToString();
                if ((leter == "#") || (leter == "♭"))
                {
                    continue;
                }
                if (ChordDefine(i, text) != "Undefine")
                {
                    for (int j = 0; j < chords.Length; j++)
                    {
                        if (ChordDefine(i, text) == chords[j])
                        {
                            check = true;
                            if (j == 11)
                            {
                                transporatedText += chords[0];
                            }
                            else
                            {
                                transporatedText += chords[j + 1];
                                break;
                            }
                        }
                        else
                        {
                            check = false;
                        }
                    }
                }
                if (check)
                {
                    continue;
                }
                else
                {
                    transporatedText += leter;
                }

            }
            return transporatedText;
        }

        public string TransporateDown(string text)
        {
            string transporatedText = "";
            for (int i = 0; i < text.Length; i++)
            {
                bool check = false;
                string leter = text[i].ToString();
                if ((leter == "#") || (leter == "♭"))
                {
                    continue;
                }
                if (ChordDefine(i, text) != "Undefine")
                {
                    for (int j = 0; j < chords.Length; j++)
                    {
                        if (ChordDefine(i, text) == chords[j])
                        {
                            check = true;
                            if (j == 0)
                            {
                                transporatedText += chords[11];
                                break;
                            }
                            else
                            {
                                transporatedText += chords[j - 1];
                                break;
                            }
                        }
                        else
                        {
                            check = false;
                        }
                    }
                }
                if (check)
                {
                    continue;
                }
                else
                {
                    transporatedText += leter;
                }
            }
            return transporatedText;

        }
    }
}
