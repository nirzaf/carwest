namespace pos
{
    internal class amountByName
    {
        private string amountName, oneDecimal, last = "Rupees Only", twoDecimal, twoDecimalTenToTwenty;
        private char[] charArray;
        private string[] stringArray;

        private void setCentsLast(string amount)
        {
            charArray = amount.ToString().ToCharArray();
            if (charArray[0].ToString().Equals("1"))
            {
                last = "Rupees " + getTentoTwenty(amount) + "Cents Only";
            }
            else
            {
                charArray = amount.ToString().ToCharArray();
                last = "Rupees " + gettwoDecimal(charArray[0].ToString()) + getOneDecimal(charArray[1].ToString()) + "Cents Only";
            }
        }

        public string setAmountName(string amount)
        {
            last = "Rupees Only";
            amountName = "";
            stringArray = (string.Format("{0:0.00}", double.Parse(amount))).ToString().Split('.');
            if (stringArray.Length != 1)
            {
                amount = stringArray[0].ToString();
                if (int.Parse(stringArray[1].ToString()) != 0)
                {
                    setCentsLast(stringArray[1].ToString());
                }
            }
            if (amount.Length == 0)
            {
                amountName = "";
            }
            else if (int.Parse(amount) == 0)
            {
                amount = "";
            }
            else if (amount.Length == 1)
            {
                amountName = getOneDecimal(amount) + last;
            }
            else if (amount.Length == 2)
            {
                charArray = amount.ToString().ToCharArray();
                if (charArray[0].ToString().Equals("1"))
                {
                    amountName = getTentoTwenty(amount) + last;
                }
                else
                {
                    charArray = amount.ToString().ToCharArray();
                    amountName = gettwoDecimal(charArray[0].ToString()) + getOneDecimal(charArray[1].ToString()) + last;
                }
            }
            else if (amount.Length == 3)
            {
                charArray = amount.ToString().ToCharArray();
                if (charArray[1].ToString().Equals("0") & charArray[2].ToString().Equals("0"))
                {
                    amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + last;
                }
                else if (charArray[1].ToString().Equals("1"))
                {
                    amountName = getOneDecimal(charArray[0].ToString()) + "Hundred and " + getTentoTwenty(charArray[1].ToString() + charArray[2].ToString()) + last;
                }
                else
                {
                    amountName = getOneDecimal(charArray[0].ToString()) + "Hundred and " + gettwoDecimal(charArray[1].ToString()) + getOneDecimal(charArray[2].ToString()) + last;
                }
            }
            else if (amount.Length == 4)
            {
                charArray = amount.ToString().ToCharArray();
                if (charArray[1].ToString().Equals("0") & charArray[2].ToString().Equals("0") & charArray[3].ToString().Equals("0"))
                {
                    amountName = getOneDecimal(charArray[0].ToString()) + "Thousand " + last;
                }
                else if (charArray[1].ToString().Equals("0") & charArray[2].ToString().Equals("1"))
                {
                    amountName = getOneDecimal(charArray[0].ToString()) + "Thousand and " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + last;
                }
                else if (charArray[1].ToString().Equals("0"))
                {
                    amountName = getOneDecimal(charArray[0].ToString()) + "Thousand and " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + last;
                }
                else if (charArray[2].ToString().Equals("0") & charArray[3].ToString().Equals("0"))
                {
                    amountName = getOneDecimal(charArray[0].ToString()) + "Thousand and " + getOneDecimal(charArray[1].ToString()) + "Hundred " + last;
                }
                else if (charArray[2].ToString().Equals("1"))
                {
                    amountName = getOneDecimal(charArray[0].ToString()) + "Thousand ," + getOneDecimal(charArray[1].ToString()) + "Hundred and " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + last;
                }
                else
                {
                    amountName = getOneDecimal(charArray[0].ToString()) + "Thousand ," + getOneDecimal(charArray[1].ToString()) + "Hundred and " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + last;
                }
            }
            else if (amount.Length == 5)
            {
                charArray = amount.ToString().ToCharArray();
                if (charArray[0].ToString().Equals("1"))
                {
                    if (charArray[2].ToString().Equals("0") & charArray[3].ToString().Equals("0") & charArray[4].ToString().Equals("0"))
                    {
                        amountName = getTentoTwenty(charArray[0].ToString() + charArray[1].ToString()) + "Thousand " + last;
                    }
                    else if (charArray[2].ToString().Equals("0") & charArray[3].ToString().Equals("1"))
                    {
                        amountName = getTentoTwenty(charArray[0].ToString() + charArray[1].ToString()) + "Thousand and " + getTentoTwenty(charArray[3].ToString() + charArray[4].ToString()) + last;
                    }
                    else if (charArray[2].ToString().Equals("0"))
                    {
                        amountName = getTentoTwenty(charArray[0].ToString() + charArray[1].ToString()) + "Thousand and " + gettwoDecimal(charArray[3].ToString()) + getOneDecimal(charArray[4].ToString()) + last;
                    }
                    else if (charArray[3].ToString().Equals("0") & charArray[4].ToString().Equals("0"))
                    {
                        amountName = getTentoTwenty(charArray[0].ToString() + charArray[1].ToString()) + "Thousand and " + getOneDecimal(charArray[2].ToString()) + "Hundred " + last;
                    }
                    else if (charArray[3].ToString().Equals("1"))
                    {
                        amountName = getTentoTwenty(charArray[0].ToString() + charArray[1].ToString()) + "Thousand ," + getOneDecimal(charArray[2].ToString()) + "Hundred and " + getTentoTwenty(charArray[3].ToString() + charArray[4].ToString()) + last;
                    }
                    else
                    {
                        amountName = getTentoTwenty(charArray[0].ToString() + charArray[1].ToString()) + "Thousand ," + getOneDecimal(charArray[2].ToString()) + "Hundred and " + gettwoDecimal(charArray[3].ToString()) + getOneDecimal(charArray[4].ToString()) + last;
                    }
                }
                else
                {
                    if (charArray[2].ToString().Equals("0") & charArray[3].ToString().Equals("0") & charArray[4].ToString().Equals("0"))
                    {
                        amountName = gettwoDecimal(charArray[0].ToString()) + getOneDecimal(charArray[1].ToString()) + "Thousand " + last;
                    }
                    else if (charArray[2].ToString().Equals("0") & charArray[3].ToString().Equals("1"))
                    {
                        amountName = gettwoDecimal(charArray[0].ToString()) + getOneDecimal(charArray[1].ToString()) + "Thousand and " + getTentoTwenty(charArray[3].ToString() + charArray[4].ToString()) + last;
                    }
                    else if (charArray[2].ToString().Equals("0"))
                    {
                        amountName = gettwoDecimal(charArray[0].ToString()) + getOneDecimal(charArray[1].ToString()) + "Thousand and " + gettwoDecimal(charArray[3].ToString()) + getOneDecimal(charArray[4].ToString()) + last;
                    }
                    else if (charArray[3].ToString().Equals("0") & charArray[4].ToString().Equals("0"))
                    {
                        amountName = gettwoDecimal(charArray[0].ToString()) + getOneDecimal(charArray[1].ToString()) + "Thousand and " + getOneDecimal(charArray[2].ToString()) + "Hundred " + last;
                    }
                    else if (charArray[3].ToString().Equals("1"))
                    {
                        amountName = gettwoDecimal(charArray[0].ToString()) + getOneDecimal(charArray[1].ToString()) + "Thousand ," + getOneDecimal(charArray[2].ToString()) + "Hundred and " + getTentoTwenty(charArray[3].ToString() + charArray[4].ToString()) + last;
                    }
                    else
                    {
                        amountName = gettwoDecimal(charArray[0].ToString()) + getOneDecimal(charArray[1].ToString()) + "Thousand ," + getOneDecimal(charArray[2].ToString()) + "Hundred and " + gettwoDecimal(charArray[3].ToString()) + getOneDecimal(charArray[4].ToString()) + last;
                    }
                }
            }
            else if (amount.Length == 6)
            {
                charArray = amount.ToString().ToCharArray();
                if (charArray[1].ToString().Equals("0") & charArray[2].ToString().Equals("0"))
                {
                    if (charArray[3].ToString().Equals("0") & charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred Thousand " + last;
                    }
                    else if (charArray[3].ToString().Equals("0") & charArray[4].ToString().Equals("1"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred Thousand and " + getTentoTwenty(charArray[4].ToString() + charArray[5].ToString()) + last;
                    }
                    else if (charArray[3].ToString().Equals("0"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred Thousand and " + gettwoDecimal(charArray[4].ToString()) + getOneDecimal(charArray[5].ToString()) + last;
                    }
                    else if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred Thousand and " + getOneDecimal(charArray[3].ToString()) + "Hundred " + last;
                    }
                    else if (charArray[4].ToString().Equals("1"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred Thousand ," + getOneDecimal(charArray[3].ToString()) + "Hundred and " + getTentoTwenty(charArray[4].ToString() + charArray[5].ToString()) + last;
                    }
                    else
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred Thousand ," + getOneDecimal(charArray[3].ToString()) + "Hundred and " + gettwoDecimal(charArray[4].ToString()) + getOneDecimal(charArray[5].ToString()) + last;
                    }
                }
                else if (charArray[1].ToString().Equals("1"))
                {
                    if (charArray[3].ToString().Equals("0") & charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + getTentoTwenty(charArray[1].ToString() + charArray[2].ToString()) + "Thousand " + last;
                    }
                    else if (charArray[3].ToString().Equals("0") & charArray[4].ToString().Equals("1"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + getTentoTwenty(charArray[1].ToString() + charArray[2].ToString()) + "Thousand and " + getTentoTwenty(charArray[4].ToString() + charArray[5].ToString()) + last;
                    }
                    else if (charArray[3].ToString().Equals("0"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + getTentoTwenty(charArray[1].ToString() + charArray[2].ToString()) + "Thousand and " + gettwoDecimal(charArray[4].ToString()) + getOneDecimal(charArray[5].ToString()) + last;
                    }
                    else if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + getTentoTwenty(charArray[1].ToString() + charArray[2].ToString()) + "Thousand and " + getOneDecimal(charArray[3].ToString()) + "Hundred " + last;
                    }
                    else if (charArray[4].ToString().Equals("1"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + getTentoTwenty(charArray[1].ToString() + charArray[2].ToString()) + "Thousand ," + getOneDecimal(charArray[3].ToString()) + "Hundred and " + getTentoTwenty(charArray[4].ToString() + charArray[5].ToString()) + last;
                    }
                    else
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + getTentoTwenty(charArray[1].ToString() + charArray[2].ToString()) + "Thousand ," + getOneDecimal(charArray[3].ToString()) + "Hundred and " + gettwoDecimal(charArray[4].ToString()) + getOneDecimal(charArray[5].ToString()) + last;
                    }
                }
                else
                {
                    if (charArray[3].ToString().Equals("0") & charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + gettwoDecimal(charArray[1].ToString()) + getOneDecimal(charArray[2].ToString()) + "Thousand " + last;
                    }
                    else if (charArray[3].ToString().Equals("0") & charArray[4].ToString().Equals("1"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + gettwoDecimal(charArray[1].ToString()) + getOneDecimal(charArray[2].ToString()) + "Thousand and " + getTentoTwenty(charArray[4].ToString() + charArray[5].ToString()) + last;
                    }
                    else if (charArray[3].ToString().Equals("0"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + gettwoDecimal(charArray[1].ToString()) + getOneDecimal(charArray[2].ToString()) + "Thousand and " + gettwoDecimal(charArray[4].ToString()) + getOneDecimal(charArray[5].ToString()) + last;
                    }
                    else if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + gettwoDecimal(charArray[1].ToString()) + getOneDecimal(charArray[2].ToString()) + "Thousand and " + getOneDecimal(charArray[3].ToString()) + "Hundred " + last;
                    }
                    else if (charArray[4].ToString().Equals("1"))
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + gettwoDecimal(charArray[1].ToString()) + getOneDecimal(charArray[2].ToString()) + "Thousand ," + getOneDecimal(charArray[3].ToString()) + "Hundred and " + getTentoTwenty(charArray[4].ToString() + charArray[5].ToString()) + last;
                    }
                    else
                    {
                        amountName = getOneDecimal(charArray[0].ToString()) + "Hundred " + gettwoDecimal(charArray[1].ToString()) + getOneDecimal(charArray[2].ToString()) + "Thousand ," + getOneDecimal(charArray[3].ToString()) + "Hundred and " + gettwoDecimal(charArray[4].ToString()) + getOneDecimal(charArray[5].ToString()) + last;
                    }
                }
            }
            else if (amount.Length == 7)
            {
                charArray = amount.ToString().ToCharArray();
                if (charArray[1].ToString().Equals("0"))
                {
                    if (charArray[2].ToString().Equals("0") & charArray[3].ToString().Equals("0"))
                    {
                        if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0") & charArray[6].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + last;
                        }
                        else if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else if (charArray[4].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                        else if (charArray[5].ToString().Equals("0") & charArray[6].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[4].ToString()) + "Hundred " + last;
                        }
                        else if (charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[4].ToString()) + "Hundred and " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[4].ToString()) + "Hundred and " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                    }
                    else if (charArray[2].ToString().Equals("1"))
                    {
                        if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0") & charArray[6].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand " + last;
                        }
                        else if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand and " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else if (charArray[4].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand and " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                        else if (charArray[5].ToString().Equals("0") & charArray[6].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand and " + getOneDecimal(charArray[4].ToString()) + "Hundred " + last;
                        }
                        else if (charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand ," + getOneDecimal(charArray[4].ToString()) + "Hundred and " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand ," + getOneDecimal(charArray[4].ToString()) + "Hundred and " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                    }
                    else
                    {
                        if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0") & charArray[6].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand " + last;
                        }
                        else if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand and " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else if (charArray[4].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand and " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                        else if (charArray[5].ToString().Equals("0") & charArray[5].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand and " + getOneDecimal(charArray[4].ToString()) + "Hundred " + last;
                        }
                        else if (charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand ," + getOneDecimal(charArray[4].ToString()) + "Hundred and " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand ," + getOneDecimal(charArray[4].ToString()) + "Hundred and " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                    }
                }
                else
                {
                    if (charArray[2].ToString().Equals("0") & charArray[3].ToString().Equals("0"))
                    {
                        if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0") & charArray[6].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred Thousand " + last;
                        }
                        else if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred Thousand and " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else if (charArray[4].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred Thousand and " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                        else if (charArray[5].ToString().Equals("0") & charArray[6].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred Thousand and " + getOneDecimal(charArray[4].ToString()) + "Hundred " + last;
                        }
                        else if (charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred Thousand ," + getOneDecimal(charArray[4].ToString()) + "Hundred and " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred Thousand ," + getOneDecimal(charArray[4].ToString()) + "Hundred and " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                    }
                    else if (charArray[2].ToString().Equals("1"))
                    {
                        if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0") & charArray[6].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand " + last;
                        }
                        else if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand and " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else if (charArray[4].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand and " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                        else if (charArray[5].ToString().Equals("0") & charArray[6].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand and " + getOneDecimal(charArray[4].ToString()) + "Hundred " + last;
                        }
                        else if (charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand ," + getOneDecimal(charArray[4].ToString()) + "Hundred and " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + getTentoTwenty(charArray[2].ToString() + charArray[3].ToString()) + "Thousand ," + getOneDecimal(charArray[4].ToString()) + "Hundred and " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                    }
                    else
                    {
                        if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("0") & charArray[6].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand " + last;
                        }
                        else if (charArray[4].ToString().Equals("0") & charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand and " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else if (charArray[4].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand and " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                        else if (charArray[5].ToString().Equals("0") & charArray[5].ToString().Equals("0"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand and " + getOneDecimal(charArray[4].ToString()) + "Hundred " + last;
                        }
                        else if (charArray[5].ToString().Equals("1"))
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand ," + getOneDecimal(charArray[4].ToString()) + "Hundred and " + getTentoTwenty(charArray[5].ToString() + charArray[6].ToString()) + last;
                        }
                        else
                        {
                            amountName = getOneDecimal(charArray[0].ToString()) + "Million " + getOneDecimal(charArray[1].ToString()) + "Hundred " + gettwoDecimal(charArray[2].ToString()) + getOneDecimal(charArray[3].ToString()) + "Thousand ," + getOneDecimal(charArray[4].ToString()) + "Hundred and " + gettwoDecimal(charArray[5].ToString()) + getOneDecimal(charArray[6].ToString()) + last;
                        }
                    }
                }
            }
            return amountName;
        }

        private string getTentoTwenty(string amount)
        {
            if (amount.Equals("10"))
            {
                twoDecimalTenToTwenty = "Ten ";
            }
            else if (amount.Equals("11"))
            {
                twoDecimalTenToTwenty = "Eleven ";
            }
            else if (amount.Equals("12"))
            {
                twoDecimalTenToTwenty = "Twelve ";
            }
            else if (amount.Equals("13"))
            {
                twoDecimalTenToTwenty = "Thirteen ";
            }
            else if (amount.Equals("14"))
            {
                twoDecimalTenToTwenty = "Fourteen ";
            }
            else if (amount.Equals("15"))
            {
                twoDecimalTenToTwenty = "Fifteen ";
            }
            else if (amount.Equals("16"))
            {
                twoDecimalTenToTwenty = "Sixteen ";
            }
            else if (amount.Equals("17"))
            {
                twoDecimalTenToTwenty = "Seventeen ";
            }
            else if (amount.Equals("18"))
            {
                twoDecimalTenToTwenty = "Eighteen ";
            }
            else if (amount.Equals("19"))
            {
                twoDecimalTenToTwenty = "Nineteen ";
            }
            else if (amount.Equals("19"))
            {
                twoDecimalTenToTwenty = "Nineteen ";
            }

            return twoDecimalTenToTwenty;
        }

        private string getOneDecimal(string amount)
        {
            if (amount.Equals("0"))
            {
                oneDecimal = " ";
            }
            else if (amount.Equals("1"))
            {
                oneDecimal = "One ";
            }
            else if (amount.Equals("2"))
            {
                oneDecimal = "Two ";
            }
            else if (amount.Equals("3"))
            {
                oneDecimal = "Three ";
            }
            else if (amount.Equals("4"))
            {
                oneDecimal = "Four ";
            }
            else if (amount.Equals("5"))
            {
                oneDecimal = "Five ";
            }
            else if (amount.Equals("6"))
            {
                oneDecimal = "Six ";
            }
            else if (amount.Equals("7"))
            {
                oneDecimal = "Seven ";
            }
            else if (amount.Equals("8"))
            {
                oneDecimal = "Eight ";
            }
            else if (amount.Equals("9"))
            {
                oneDecimal = "Nine ";
            }

            return oneDecimal;
        }

        private string gettwoDecimal(string amount)
        {
            if (amount.Equals("0"))
            {
                twoDecimal = " ";
            }
            else if (amount.Equals("1"))
            {
                twoDecimal = "Ten ";
            }
            else if (amount.Equals("2"))
            {
                twoDecimal = "Twenty ";
            }
            else if (amount.Equals("3"))
            {
                twoDecimal = "Thirty ";
            }
            else if (amount.Equals("4"))
            {
                twoDecimal = "Forty ";
            }
            else if (amount.Equals("5"))
            {
                twoDecimal = "Fifty ";
            }
            else if (amount.Equals("6"))
            {
                twoDecimal = "Sixty ";
            }
            else if (amount.Equals("7"))
            {
                twoDecimal = "Seventy ";
            }
            else if (amount.Equals("8"))
            {
                twoDecimal = "Eighty ";
            }
            else if (amount.Equals("9"))
            {
                twoDecimal = "Ninety ";
            }

            return twoDecimal;
        }
    }
}