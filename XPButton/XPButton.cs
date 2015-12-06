﻿#region License
//   Copyright 2015 Brook Shi
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace XP
{
    public sealed class XPButton : Button
    {
        private ContentControl _symbol;
        private Viewbox _symbolView;
        private RelativePanel _visualPanel;
        private ContentPresenter _contentPresenter;
        private long[] _propertyRegTokens = new long[13];
        private object[] _backupObjects = new object[13];
        private bool _isLoadedCompleted = false;

        #region property

        public event ToggleEvent OnToggleChanged;

        public bool IsToggleMode
        {
            get { return (bool)GetValue(IsToggleModeProperty); }
            set { SetValue(IsToggleModeProperty, value); }
        }
        public static readonly DependencyProperty IsToggleModeProperty =
            DependencyProperty.Register("IsToggleMode", typeof(bool), typeof(XPButton), new PropertyMetadata(false));

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(XPButton), new PropertyMetadata(false, (s, d)=> 
            {
                var btn = s as XPButton;
                if (btn._isLoadedCompleted)
                    btn.SwitchBrush();
            }));

        public IconElement Icon
        {
            get { return (IconElement)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(IconElement), typeof(XPButton), null);

        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(XPButton), new PropertyMetadata(20d));

        public double IconInterval
        {
            get { return (double)GetValue(IconIntervalProperty); }
            set { SetValue(IconIntervalProperty, value); }
        }
        public static readonly DependencyProperty IconIntervalProperty =
            DependencyProperty.Register("IconInterval", typeof(double), typeof(XPButton), new PropertyMetadata(5d));

        public IconPosition IconPosition
        {
            get { return (IconPosition)GetValue(IconPositionProperty); }
            set { SetValue(IconPositionProperty, value); }
        }
        public static readonly DependencyProperty IconPositionProperty =
            DependencyProperty.Register("IconPosition", typeof(IconPosition), typeof(XPButton), new PropertyMetadata(IconPosition.Left));

        public Brush IconForeground
        {
            get { return (Brush)GetValue(IconForegroundProperty); }
            set { SetValue(IconForegroundProperty, value); }
        }
        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register("IconForeground", typeof(Brush), typeof(XPButton), null);

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(XPButton), new PropertyMetadata(new CornerRadius(0)));

        public Brush PointerOverBackground
        {
            get { return (Brush)GetValue(PointerOverBackgroundProperty); }
            set { SetValue(PointerOverBackgroundProperty, value); }
        }
        public static readonly DependencyProperty PointerOverBackgroundProperty =
            DependencyProperty.Register("PointerOverBackground", typeof(Brush), typeof(XPButton), null);

        public Brush PointerOverTextForeground
        {
            get { return (Brush)GetValue(PointerOverTextForegroundProperty); }
            set { SetValue(PointerOverTextForegroundProperty, value); }
        }
        public static readonly DependencyProperty PointerOverTextForegroundProperty =
            DependencyProperty.Register("PointerOverTextForeground", typeof(Brush), typeof(XPButton), null);

        public Brush PointerOverIconForeground
        {
            get { return (Brush)GetValue(PointerOverIconForegroundProperty); }
            set { SetValue(PointerOverIconForegroundProperty, value); }
        }
        public static readonly DependencyProperty PointerOverIconForegroundProperty =
            DependencyProperty.Register("PointerOverIconForeground", typeof(Brush), typeof(XPButton), null);

        public Brush PointerOverBorderBrush
        {
            get { return (Brush)GetValue(PointerOverBorderBrushProperty); }
            set { SetValue(PointerOverBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty PointerOverBorderBrushProperty =
            DependencyProperty.Register("PointerOverBorderBrush", typeof(Brush), typeof(XPButton), null);

        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(XPButton), null);

        public Brush PressedTextForeground
        {
            get { return (Brush)GetValue(PressedTextForegroundProperty); }
            set { SetValue(PressedTextForegroundProperty, value); }
        }
        public static readonly DependencyProperty PressedTextForegroundProperty =
            DependencyProperty.Register("PressedTextForeground", typeof(Brush), typeof(XPButton), null);

        public Brush PressedIconForeground
        {
            get { return (Brush)GetValue(PressedIconForegroundProperty); }
            set { SetValue(PressedIconForegroundProperty, value); }
        }
        public static readonly DependencyProperty PressedIconForegroundProperty =
            DependencyProperty.Register("PressedIconForeground", typeof(Brush), typeof(XPButton), null);

        public Brush PressedBorderBrush
        {
            get { return (Brush)GetValue(PressedBorderBrushProperty); }
            set { SetValue(PressedBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.Register("PresseddBorderBrush", typeof(Brush), typeof(XPButton), null);

        public Brush DisabledBackground
        {
            get { return (Brush)GetValue(DisabledBackgroundProperty); }
            set { SetValue(DisabledBackgroundProperty, value); }
        }
        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register("DisabledBackground", typeof(Brush), typeof(XPButton), null);

        public Brush DisabledTextForeground
        {
            get { return (Brush)GetValue(DisabledTextForegroundProperty); }
            set { SetValue(DisabledTextForegroundProperty, value); }
        }
        public static readonly DependencyProperty DisabledTextForegroundProperty =
            DependencyProperty.Register("DisabledTextForeground", typeof(Brush), typeof(XPButton), null);

        public Brush DisabledIconForeground
        {
            get { return (Brush)GetValue(DisabledIconForegroundProperty); }
            set { SetValue(DisabledIconForegroundProperty, value); }
        }
        public static readonly DependencyProperty DisabledIconForegroundProperty =
            DependencyProperty.Register("DisabledIconForeground", typeof(Brush), typeof(XPButton), null);

        public Brush DisabledBorderBrush
        {
            get { return (Brush)GetValue(DisabledBorderBrushProperty); }
            set { SetValue(DisabledBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.Register("DisabledBorderBrush", typeof(Brush), typeof(XPButton), null);

        public string CheckedContent
        {
            get { return (string)GetValue(CheckedContentProperty); }
            set { SetValue(CheckedContentProperty, value); }
        }
        public static readonly DependencyProperty CheckedContentProperty =
            DependencyProperty.Register("CheckedContent", typeof(string), typeof(XPButton), new PropertyMetadata(""));

        public Brush CheckedBackground
        {
            get { return (Brush)GetValue(CheckedBackgroundProperty); }
            set { SetValue(CheckedBackgroundProperty, value); }
        }
        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(XPButton), null);

        public Brush CheckedTextForeground
        {
            get { return (Brush)GetValue(CheckedTextForegroundProperty); }
            set { SetValue(CheckedTextForegroundProperty, value); }
        }
        public static readonly DependencyProperty CheckedTextForegroundProperty =
            DependencyProperty.Register("CheckedTextForeground", typeof(Brush), typeof(XPButton), null);

        public Brush CheckedIconForeground
        {
            get { return (Brush)GetValue(CheckedIconForegroundProperty); }
            set { SetValue(CheckedIconForegroundProperty, value); }
        }
        public static readonly DependencyProperty CheckedIconForegroundProperty =
            DependencyProperty.Register("CheckedIconForeground", typeof(Brush), typeof(XPButton), null);

        public Brush CheckedBorderBrush
        {
            get { return (Brush)GetValue(CheckedBorderBrushProperty); }
            set { SetValue(CheckedBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty CheckedBorderBrushProperty =
            DependencyProperty.Register("CheckedBorderBrush", typeof(Brush), typeof(XPButton), null);

        public Brush CheckedPointerOverBackground
        {
            get { return (Brush)GetValue(CheckedPointerOverBackgroundProperty); }
            set { SetValue(CheckedPointerOverBackgroundProperty, value); }
        }
        public static readonly DependencyProperty CheckedPointerOverBackgroundProperty =
            DependencyProperty.Register("CheckedPointerOverBackground", typeof(Brush), typeof(XPButton), null);

        public Brush CheckedPointerOverTextForeground
        {
            get { return (Brush)GetValue(CheckedPointerOverTextForegroundProperty); }
            set { SetValue(CheckedPointerOverTextForegroundProperty, value); }
        }
        public static readonly DependencyProperty CheckedPointerOverTextForegroundProperty =
            DependencyProperty.Register("CheckedPointerOverTextForeground", typeof(Brush), typeof(XPButton), null);

        public Brush CheckedPointerOverIconForeground
        {
            get { return (Brush)GetValue(CheckedPointerOverIconForegroundProperty); }
            set { SetValue(CheckedPointerOverIconForegroundProperty, value); }
        }
        public static readonly DependencyProperty CheckedPointerOverIconForegroundProperty =
            DependencyProperty.Register("CheckedPointerOverIconForeground", typeof(Brush), typeof(XPButton), null);

        public Brush CheckedPointerOverBorderBrush
        {
            get { return (Brush)GetValue(CheckedPointerOverBorderBrushProperty); }
            set { SetValue(CheckedPointerOverBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty CheckedPointerOverBorderBrushProperty =
            DependencyProperty.Register("CheckedPointerOverBorderBrush", typeof(Brush), typeof(XPButton), null);

        public Brush CheckedPressedBackground
        {
            get { return (Brush)GetValue(CheckedPressedBackgroundProperty); }
            set { SetValue(CheckedPressedBackgroundProperty, value); }
        }
        public static readonly DependencyProperty CheckedPressedBackgroundProperty =
            DependencyProperty.Register("CheckedPressedBackground", typeof(Brush), typeof(XPButton), null);

        public Brush CheckedPressedTextForeground
        {
            get { return (Brush)GetValue(CheckedPressedTextForegroundProperty); }
            set { SetValue(CheckedPressedTextForegroundProperty, value); }
        }
        public static readonly DependencyProperty CheckedPressedTextForegroundProperty =
            DependencyProperty.Register("CheckedPressedTextForeground", typeof(Brush), typeof(XPButton), null);

        public Brush CheckedPressedIconForeground
        {
            get { return (Brush)GetValue(CheckedPressedIconForegroundProperty); }
            set { SetValue(CheckedPressedIconForegroundProperty, value); }
        }
        public static readonly DependencyProperty CheckedPressedIconForegroundProperty =
            DependencyProperty.Register("CheckedPressedIconForeground", typeof(Brush), typeof(XPButton), null);

        public Brush CheckedPressedBorderBrush
        {
            get { return (Brush)GetValue(CheckedPressedBorderBrushProperty); }
            set { SetValue(CheckedPressedBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty CheckedPressedBorderBrushProperty =
            DependencyProperty.Register("CheckedPresseddBorderBrush", typeof(Brush), typeof(XPButton), null);

        #endregion

        #region adjust content

        void HorizontalCenterElements()
        {
            if (_symbolView == null || _contentPresenter == null)
                return;

            switch (IconPosition)
            {
                case IconPosition.Left:
                    _symbolView.Margin = new Thickness(CalculateMarginWidth(), 0, 0, 0);
                    break;
                case IconPosition.Right:
                    _symbolView.Margin = new Thickness(0, 0, CalculateMarginWidth(), 0);
                    break;
                case IconPosition.Top:
                    _symbolView.Margin = new Thickness(0, CalculateMarginHeight(), 0, 0);
                    break;
                case IconPosition.Bottom:
                    _symbolView.Margin = new Thickness(0, 0, 0, CalculateMarginHeight());
                    break;
            }
        }

        double CalculateMarginWidth()
        {
            var buttonPaddingWidth = Padding.Left + Padding.Right;
            var marginWidth = (ActualWidth - IconInterval - _symbolView.DesiredSize.Width - _contentPresenter.DesiredSize.Width - buttonPaddingWidth) / 2;
            return Math.Max(0, marginWidth);
        }

        double CalculateMarginHeight()
        {
            var buttonPaddingHeight = Padding.Top + Padding.Bottom;
            var marginHeight = (ActualHeight - IconInterval - _symbolView.DesiredSize.Height - _contentPresenter.DesiredSize.Height - buttonPaddingHeight) / 2;
            return Math.Max(0, marginHeight);
        }

        #endregion

        public XPButton()
        {
            this.DefaultStyleKey = typeof(XPButton);
            Loaded += XPButton_Loaded;
        }

        private void XPButton_Loaded(object sender, RoutedEventArgs e)
        {
            InitPropertyForNull();

            RegisterPropertyChangedCallbacks();

            HorizontalCenterElements();

            BackupBrush();

            SwitchBrush();

            _isLoadedCompleted = true;
        }

        private void InitPropertyForNull()
        {
            if (IconForeground == null) IconForeground = Foreground;

            if (PointerOverBackground == null) PointerOverBackground = Background;
            if (PointerOverTextForeground == null) PointerOverTextForeground = Foreground;
            if (PointerOverIconForeground == null) PointerOverIconForeground = Foreground;
            if (PointerOverBorderBrush == null) PointerOverBorderBrush = BorderBrush;

            if (PressedBackground == null) PressedBackground = Background;
            if (PressedTextForeground == null) PressedTextForeground = Foreground;
            if (PressedIconForeground == null) PressedIconForeground = Foreground;
            if (PressedBorderBrush == null) PressedBorderBrush = BorderBrush;

            if (DisabledBackground == null) DisabledBackground = Background;
            if (DisabledTextForeground == null) DisabledTextForeground = Foreground;
            if (DisabledIconForeground == null) DisabledIconForeground = Foreground;
            if (DisabledBorderBrush == null) DisabledBorderBrush = BorderBrush;

            if (CheckedPointerOverBackground == null) CheckedPointerOverBackground = CheckedBackground;
            if (CheckedPointerOverTextForeground == null) CheckedPointerOverTextForeground = CheckedTextForeground;
            if (CheckedPointerOverIconForeground == null) CheckedPointerOverIconForeground = CheckedIconForeground;
            if (CheckedPointerOverBorderBrush == null) CheckedPointerOverBorderBrush = CheckedBorderBrush;

            if (CheckedPressedBackground == null) CheckedPressedBackground = CheckedBackground;
            if (CheckedPressedTextForeground == null) CheckedPressedTextForeground = CheckedTextForeground;
            if (CheckedPressedIconForeground == null) CheckedPressedIconForeground = CheckedIconForeground;
            if (CheckedPressedBorderBrush == null) CheckedPressedBorderBrush = CheckedBorderBrush;

            if (CheckedContent == null) CheckedContent = Content.ToString();
        }

        private void RegisterPropertyChangedCallbacks()
        {
            _propertyRegTokens[0] = RegisterPropertyChangedCallback(ContentProperty, (s, d) => { _backupObjects[0]  = Content; });
            _propertyRegTokens[1] = RegisterPropertyChangedCallback(IconForegroundProperty, (s, d) => { _backupObjects[1]  = IconForeground; });
            _propertyRegTokens[2] = RegisterPropertyChangedCallback(ForegroundProperty, (s, d) => { _backupObjects[2]  = Foreground; });
            _propertyRegTokens[3] = RegisterPropertyChangedCallback(BackgroundProperty, (s, d) => { _backupObjects[3]  = Background; });
            _propertyRegTokens[4] = RegisterPropertyChangedCallback(BorderBrushProperty, (s, d) => { _backupObjects[4]  = BorderBrush; });
            _propertyRegTokens[5] = RegisterPropertyChangedCallback(PointerOverBackgroundProperty, (s, d) => { _backupObjects[5]  = PointerOverBackground; });
            _propertyRegTokens[6] = RegisterPropertyChangedCallback(PointerOverTextForegroundProperty, (s, d) => { _backupObjects[6]  = PointerOverTextForeground; });
            _propertyRegTokens[7] = RegisterPropertyChangedCallback(PointerOverIconForegroundProperty, (s, d) => { _backupObjects[7]  = PointerOverIconForeground; });
            _propertyRegTokens[8] = RegisterPropertyChangedCallback(PointerOverBorderBrushProperty, (s, d) => { _backupObjects[8]  = PointerOverBorderBrush; });
            _propertyRegTokens[9] = RegisterPropertyChangedCallback(PressedBackgroundProperty, (s, d) => { _backupObjects[9]  = PressedBackground; });
            _propertyRegTokens[10] = RegisterPropertyChangedCallback(PressedTextForegroundProperty, (s, d) => { _backupObjects[10] = PressedTextForeground; });
            _propertyRegTokens[11] = RegisterPropertyChangedCallback(PressedIconForegroundProperty, (s, d) => { _backupObjects[11] = PressedIconForeground; });
            _propertyRegTokens[12] = RegisterPropertyChangedCallback(PressedBorderBrushProperty, (s, d) => { _backupObjects[12] = PressedBorderBrush; });
        }

        private void UnregisterPropertyChangedCallbacks()
        {
            UnregisterPropertyChangedCallback(ContentProperty, _propertyRegTokens[0]);
            UnregisterPropertyChangedCallback(IconForegroundProperty, _propertyRegTokens[1]);
            UnregisterPropertyChangedCallback(ForegroundProperty, _propertyRegTokens[2]);
            UnregisterPropertyChangedCallback(BackgroundProperty, _propertyRegTokens[3]);
            UnregisterPropertyChangedCallback(BorderBrushProperty, _propertyRegTokens[4]);
            UnregisterPropertyChangedCallback(PointerOverBackgroundProperty, _propertyRegTokens[5]);
            UnregisterPropertyChangedCallback(PointerOverTextForegroundProperty, _propertyRegTokens[6]);
            UnregisterPropertyChangedCallback(PointerOverIconForegroundProperty, _propertyRegTokens[7]);
            UnregisterPropertyChangedCallback(PointerOverBorderBrushProperty, _propertyRegTokens[8]);
            UnregisterPropertyChangedCallback(PressedBackgroundProperty, _propertyRegTokens[9]);
            UnregisterPropertyChangedCallback(PressedTextForegroundProperty, _propertyRegTokens[10]);
            UnregisterPropertyChangedCallback(PressedIconForegroundProperty, _propertyRegTokens[11]);
            UnregisterPropertyChangedCallback(PressedBorderBrushProperty, _propertyRegTokens[12]);
        }

        private long RegisterPropertyChangedCallback(DependencyProperty property, object backupProperty, object originProperty)
        {
            return RegisterPropertyChangedCallback(property, (s, d) =>
            {
                backupProperty = originProperty;
            });
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _visualPanel = (RelativePanel)GetTemplateChild("VisualPanel");
            _symbol = (ContentControl)GetTemplateChild("Symbol");
            _symbolView = (Viewbox)GetTemplateChild("SymbolView");
            _contentPresenter = (ContentPresenter)GetTemplateChild("ContentPresenter");
        }

        private void BackupBrush()
        {
            _backupObjects[0] = Content;
            _backupObjects[1] = IconForeground;
            _backupObjects[2] = Foreground;
            _backupObjects[3] = Background;
            _backupObjects[4] = BorderBrush;
            _backupObjects[5] = PointerOverBackground;
            _backupObjects[6] = PointerOverTextForeground;
            _backupObjects[7] = PointerOverIconForeground;
            _backupObjects[8] = PointerOverBorderBrush;
            _backupObjects[9] = PressedBackground;
            _backupObjects[10] = PressedTextForeground;
            _backupObjects[11] = PressedIconForeground;
            _backupObjects[12] = PressedBorderBrush;
        }

        private void SwitchBrush()
        {
            UnregisterPropertyChangedCallbacks();

            Content = IsChecked ? CheckedContent : _backupObjects[0];
            IconForeground = IsChecked ? CheckedIconForeground : (Brush)_backupObjects[1];
            Foreground = IsChecked ? CheckedTextForeground : (Brush)_backupObjects[2];
            Background = IsChecked ? CheckedBackground : (Brush)_backupObjects[3];
            BorderBrush = IsChecked ? CheckedBorderBrush : (Brush)_backupObjects[4];
            PointerOverBackground = IsChecked ? CheckedPointerOverBackground : (Brush)_backupObjects[5];
            PointerOverTextForeground = IsChecked ? CheckedPointerOverTextForeground : (Brush)_backupObjects[6];
            PointerOverIconForeground = IsChecked ? CheckedPointerOverIconForeground : (Brush)_backupObjects[7];
            PointerOverBorderBrush = IsChecked ? CheckedPointerOverBorderBrush : (Brush)_backupObjects[8];
            PressedBackground = IsChecked ? CheckedPressedBackground : (Brush)_backupObjects[9];
            PressedTextForeground= IsChecked ? CheckedPressedTextForeground : (Brush)_backupObjects[10];
            PressedIconForeground = IsChecked ? CheckedPressedIconForeground : (Brush)_backupObjects[11];
            PressedBorderBrush = IsChecked ? CheckedPressedBorderBrush : (Brush)_backupObjects[12];

            RegisterPropertyChangedCallbacks();
        }

        protected override void OnTapped(TappedRoutedEventArgs e)
        {
            if (!IsToggleMode)
            {
                base.OnTapped(e);
                return;
            }

            IsChecked = !IsChecked;

            if (OnToggleChanged != null)
            {
                var eventArgs = new ToggleEventArgs(IsChecked);
                OnToggleChanged(this, eventArgs);
                if (eventArgs.IsCancel)
                {
                    IsChecked = !IsChecked;
                }
            }
        }
    }
}
