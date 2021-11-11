namespace TestExercise
{
    internal interface IDialogService
    {
        
        string FilePath { get; set; }  
       
        bool SaveFileDialog();  
    }
}