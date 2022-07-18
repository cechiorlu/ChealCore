using ChealCore.Enums;

namespace ChealCore.Logic
{
    public class GLCategoryLogic
    {
        
       public long GenerateGLCategoryCodeNumber(MainAccountCategory mainAccountCategory, int id)
        {
            long codeNumber = 0;
            long Id = id;

            switch (mainAccountCategory)
            {
                case MainAccountCategory.Asset:
                    codeNumber = 1000 + Id;
                    break;

                case MainAccountCategory.Liability:
                    codeNumber = 2000 + Id;
                    break;
                case MainAccountCategory.Income:
                    codeNumber = 4000 + Id;
                    break;
                case MainAccountCategory.Capital:
                    codeNumber = 3000 + Id;
                    break;
                case MainAccountCategory.Expenses:
                    codeNumber = 5000 + Id;
                    break;
                default:
                    break;
            }
            return codeNumber;
        }

        
    }
}
