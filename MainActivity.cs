using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using XAExpandableListView.Adapters;

namespace XAExpandableListView
{
    [Activity(Label = "Expanadable ListView", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ExpandableListAdapter listAdapter;
        private ExpandableListView expandableList;
        private List<string> listHeaderData;
        private Dictionary<ExpandableGroupModel, List<ExpandableChildModel>> listChildData;
        private List<ExpandableChildModel> groupData;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            expandableList = FindViewById(Resource.Id.expandList) as ExpandableListView;

            LoadData();

            //listAdapter = new ExpandableListAdapter(this, listHeaderData, listChildData);
            listAdapter = new ExpandableListAdapter(this, listChildData);
            expandableList.SetAdapter(listAdapter);
        }

        private void LoadData()
        {
            listHeaderData = new List<string>();
            listChildData = new Dictionary<ExpandableGroupModel, List<ExpandableChildModel>>();
            groupData = new List<ExpandableChildModel>();

            listHeaderData.Add("Idea Generation");
            listHeaderData.Add("Verbal abilities");
            listHeaderData.Add("Quantitive abilities");
            listHeaderData.Add("Memory");
            listHeaderData.Add("Perceptual abilities");

            groupData.Add(new ExpandableChildModel() { Name = "Fluency of Ideas" });
            groupData.Add(new ExpandableChildModel() { Name = "Originality" });
            groupData.Add(new ExpandableChildModel() { Name = "Problem Sensitivity" });
            groupData.Add(new ExpandableChildModel() { Name = "Mathamatical Reasoning" });
            groupData.Add(new ExpandableChildModel() { Name = "Problem Sensitivity" });
            groupData.Add(new ExpandableChildModel() { Name = "Speed of closure" });
            groupData.Add(new ExpandableChildModel() { Name = "Number Facility" });
            groupData.Add(new ExpandableChildModel() { Name = "Speed of closure" });
            groupData.Add(new ExpandableChildModel() { Name = "Flexiblity of closure" });


            for (int i = 0; i < listHeaderData.Count; i++)
            {
                listChildData.Add(new ExpandableGroupModel {Name = listHeaderData[i], IsCheked = false } , groupData);
            }

        }


        //private void LoadDatas()
        //{
        //    listHeaderData = new List<string>();
        //    listChildData = new Dictionary<string, List<ExpandableChildModel>>();
        //    groupData = new List<ExpandableChildModel>();

        //    listHeaderData.Add("Idea Generation");
        //    listHeaderData.Add("Verbal abilities");
        //    listHeaderData.Add("Quantitive abilities");
        //    listHeaderData.Add("Memory");
        //    listHeaderData.Add("Perceptual abilities");

        //    groupData.Add(new ExpandableChildModel() { Name = "Fluency of Ideas", Value = "Done" });
        //    groupData.Add(new ExpandableChildModel() { Name = "Originality", Value = "Pending" });
        //    groupData.Add(new ExpandableChildModel() { Name = "Problem Sensitivity", Value = null });
        //    groupData.Add(new ExpandableChildModel() { Name = "Mathamatical Reasoning", Value = "Pending" });
        //    groupData.Add(new ExpandableChildModel() { Name = "Problem Sensitivity", Value = null });
        //    groupData.Add(new ExpandableChildModel() { Name = "Speed of closure", Value = null });
        //    groupData.Add(new ExpandableChildModel() { Name = "Number Facility", Value = null });
        //    groupData.Add(new ExpandableChildModel() { Name = "Speed of closure", Value = null });
        //    groupData.Add(new ExpandableChildModel() { Name = "Flexiblity of closure", Value = null });


        //    for (int i = 0; i < listHeaderData.Count; i++)
        //    {
        //        listChildData.Add(listHeaderData[i], groupData);
        //    }

        //}


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
