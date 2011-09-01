﻿using System;
using FlagConsole.Controls;
using FlagConsole.Measure;

namespace FlagConsole.Demo
{
    internal class DemoScreen : Screen
    {
        private enum MainMenuAction
        {
            Item1,
            Item2,
            Item3
        }

        private Menu<Action> mainMenu;

        private Panel mainMenuPanel;
        private Panel presentationPanel;

        private Label mainManuTextLabel;

        public DemoScreen()
        {
            this.mainMenuPanel = new Panel();
            this.mainMenuPanel.RelativeLocation = new Point(2, 1);
            this.Controls.Add(this.mainMenuPanel);

            this.mainManuTextLabel = new Label();
            this.mainManuTextLabel.Size = new Size(30, 5);
            this.mainManuTextLabel.Text = "This is the main menu. You can select items with the up and down arrows or the W and S keys. Press enter if you want to see the presentation.";
            this.mainMenuPanel.Controls.Add(mainManuTextLabel);

            this.mainMenu = new Menu<Action>();
            this.mainMenu.RelativeLocation = new Point(0, 6);
            this.mainMenu.Items.Add(new MenuItem<Action>("Label demo", this.ShowLabelDemo));
            this.mainMenu.Items.Add(new MenuItem<Action>("ListView demo", this.ShowListViewDemo));
            this.mainMenu.Items.Add(new MenuItem<Action>("TextField demo", this.ShowTextFieldDemo));
            this.mainMenu.Items.Add(new MenuItem<Action>("Exit", this.Exit));

            this.mainMenu.UpKeys.Add(ConsoleKey.W);
            this.mainMenu.DownKeys.Add(ConsoleKey.S);

            this.mainMenu.ItemChosen += new EventHandler<MenuEventArgs<Action>>(mainMenu_ItemChosen);

            this.mainMenuPanel.Controls.Add(this.mainMenu);
        }

        private void mainMenu_ItemChosen(object sender, MenuEventArgs<Action> e)
        {
            e.Item.Value.Invoke();
            this.mainMenu.Focus();
        }

        public void Activate()
        {
            this.Update();
            this.mainMenu.Focus();
        }

        public void SwitchDemoPanel(Panel panel)
        {
            this.Controls.Remove(this.presentationPanel);
            this.presentationPanel = panel;
            this.presentationPanel.RelativeLocation = new Point(35, 7);
            this.Controls.Add(this.presentationPanel);
            this.Update();
        }

        public void ShowLabelDemo()
        {
            this.SwitchDemoPanel(new LabelDemoPanel());
        }

        public void ShowListViewDemo()
        {
            this.SwitchDemoPanel(new ListViewDemoPanel());
        }

        public void ShowTextFieldDemo()
        {
            var panel = new TextFieldDemoPanel();
            this.SwitchDemoPanel(panel);
            panel.Activate();
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}