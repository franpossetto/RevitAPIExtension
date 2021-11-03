﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TemplateWizard;
using System.Windows;
using EnvDTE;

namespace RevitAPIExtension.Wizards
{
    public class VersionWizard:IWizard
    {
        private string revitver;
        private string revitRefPath;
        private string packageRef;
        private bool isnugetsel;
        private bool islocalsel;

        // This method is called before opening any item that
        // has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        // This method is only called for item templates,
        // not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem
            projectItem)
        {
        }

        // This method is called after the project is created.
        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            try
            {
                // Display a form to the user. The form collects
                // input for the custom message.
                VersionWizardWPF revitDataForm = new VersionWizardWPF();
                
                revitDataForm.ShowDialog();
                revitver = revitDataForm.revitAPIVersion;

                // class containing all project paths
                Path path = new Path();
                packageRef = path.PackageVersion[revitver];
                if (revitDataForm.IsNugetSel)
                {
                    // enviroment variable for C:\Users\%username%
                    string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                    // making the complete path and the package reference
                    revitRefPath = home + path.baseRefDir + path.ReferencePath[revitver];
                    
                }
                else
                {

                    // making the complete path and the package reference
                    revitRefPath = path.RevitDllPath[revitver].Replace("\\RevitAPI.dll", "");
                }
                isnugetsel = revitDataForm.IsNugetSel;

                // Add custom parameters.
                replacementsDictionary.Add("$revitRefPath$",
                    revitRefPath);
                replacementsDictionary.Add("$packageRef$",
                    packageRef);
                replacementsDictionary.Add("$revitver$",
                    revitver);
                replacementsDictionary.Add("$isnugetsel$",
                    isnugetsel.ToString());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // This method is only called for item templates,
        // not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
