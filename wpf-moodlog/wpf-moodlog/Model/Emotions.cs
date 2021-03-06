﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace wpf_moodlog.Model
{
    public class Emotions
    {
        public const int nEmotions = 6;
        private Emotion[] Names = {
            Emotion.Joy,
            Emotion.Sadness,
            Emotion.Anger,
            Emotion.Surprised,
            Emotion.Disgust,
            Emotion.Fear
        };
        public float[] Values { get; }

        public Emotions(float[] Values)
        {
            this.Values = Values;
            this.ChartUI = new PieSeries();
            this.LegendUI = new StackPanel();

            initChartUIProperties();
            initLegendUIProperties();
        }

        public Emotions(string Text)
        {
            this.Values = compute(Text);
            this.ChartUI = new PieSeries();
            this.LegendUI = new StackPanel();

            initChartUIProperties();
            initLegendUIProperties();
        }

        private float[] compute(string text)
        {
            Program program = new Program();

            float[] values = program.processText(text);

            float sum = values.Sum();

            for(int i = 0; i < values.Length; i++)
            {
                values[i] = values[i] / sum;
            }

            return values;
        }

        PieSeries _ChartUI;
        public PieSeries ChartUI
        {
            get
            {
                Chart chart = new Chart();

                chart.Series.Add(this._ChartUI);

                return this._ChartUI;
            }
            private set
            {
                this._ChartUI = value;
            }
        }

        StackPanel _LegendUI;
        public StackPanel LegendUI
        {
            get
            {
                for (int i = 0; i < nEmotions; i++)
                {
                    Grid iconUI = LegendIconUI(Values[i], Names[i].GetColor());
                    TextBlock descriptionUI = LegendDescriptionUI(Names[i].GetName());

                    addToLegendUIChildren(iconUI, descriptionUI);
                }

                return this._LegendUI;
            }
            set
            {
                this._LegendUI = value;
            }
        }

        private void initChartUIProperties()
        {
            _ChartUI.IndependentValueBinding = new Binding("Key");
            _ChartUI.DependentValueBinding = new Binding("Value");
            _ChartUI.ItemsSource = ChartUIItemsSource();

            _ChartUI.Height = 60;
            _ChartUI.Width = 60;

            _ChartUI.Margin = new Thickness(5, 0, 20, 0);
            _ChartUI.Palette = Application.Current.Resources["ChartPalette"] as Collection<ResourceDictionary>;
        }

        private void initLegendUIProperties()
        {
            _LegendUI.Orientation = Orientation.Horizontal;
            _LegendUI.Margin = new Thickness(0, 5, 0, 10);
        }

        private void addToLegendUIChildren(Grid icon, TextBlock description)
        {
            _LegendUI.Children.Add(icon);
            _LegendUI.Children.Add(description);
        }
        
        private TextBlock LegendDescriptionUI(string name)
        {
            TextBlock ui = new TextBlock()
            {
                Margin = new Thickness(3, 0, 20, 0),
                Text = name,
                VerticalAlignment = VerticalAlignment.Center,
            };

            return ui;
        }

        private Grid LegendIconUI(float value, SolidColorBrush color)
        {
            Grid ui = new Grid();

            Ellipse circle = new Ellipse()
            {
                Fill = color,
                Height = 25,
                Width = 25,
            };

            Label percent = new Label()
            {
                Content = (value * 100.0) + "%",
                FontSize = 8,
                Foreground = Brushes.White,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Width = 25,
            };

            ui.Children.Add(circle);
            ui.Children.Add(percent);

            return ui;
        }

        private Dictionary<Emotion, float> ChartUIItemsSource()
        {
            Dictionary<Emotion, float> itemsSource = new Dictionary<Emotion, float>();

            for(int i = 0; i < nEmotions; i++)
            {
                itemsSource.Add(Names[i], Values[i]);
            }

            return itemsSource;
        }
    }
}
