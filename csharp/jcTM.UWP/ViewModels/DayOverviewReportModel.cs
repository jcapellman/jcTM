﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

using Windows.UI.Xaml;

using jcTM.PCL.Transports;

namespace jcTM.UWP.ViewModels {
    public class DayOverviewReportModel : BaseModel {
        private DayOverviewListingResponseItem _selectedListItem;

        public DayOverviewListingResponseItem SelectedListItem {
            get {  return _selectedListItem; } set { _selectedListItem = value; OnPropertyChanged(); }
        }

        private List<DayOverviewListingResponseItem> _listingItems;

        public List<DayOverviewListingResponseItem> ListingItems {
            get { return _listingItems; }
            set { _listingItems = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadData() {
            GraphVisibility = Visibility.Collapsed;

            ListingItems = await GET<List<DayOverviewListingResponseItem>>("DayOverviewReport");

            return true;
        }

        private List<DayOverviewDetailResponseItem> _detailGraphItems;

        public List<DayOverviewDetailResponseItem> DetailGraphItems {
            get {  return _detailGraphItems; } set { _detailGraphItems = value; OnPropertyChanged(); }
        }

        private Visibility _graphVisibility;

        public Visibility GraphVisibility {
            get { return _graphVisibility; } set { _graphVisibility = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadGraphData() {
            var dateTime = $"{SelectedListItem.Day.Month}/{SelectedListItem.Day.Day}/{SelectedListItem.Day.Year}";

            DetailGraphItems = await GET<List<DayOverviewDetailResponseItem>>($"DayOverviewReport?selectedDay={dateTime}");

            GraphVisibility = Visibility.Visible;

            return true;
        }
    }
}