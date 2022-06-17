using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;

namespace XAExpandableListView.Adapters
{
    public class ExpandableListAdapter : BaseExpandableListAdapter
    {
        public Activity context;
        public List<string> headerList;
        public Dictionary<ExpandableGroupModel, List<ExpandableChildModel>> childList;

        
        public ExpandableListAdapter(Activity contextactivity, Dictionary<ExpandableGroupModel, List<ExpandableChildModel>> childlist)
        {
            context = contextactivity;
            childList = childlist;
            headerList = childlist.Keys.Select(x => x.Name).ToList<string>();
        }

        public override int GroupCount
        {
            get
            {
                return headerList.Count;
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return false;
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            var groupName = headerList[groupPosition];
            var groupItem = childList.FirstOrDefault(x => x.Key.Name == groupName);
            return groupItem.Value[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            var groupName = headerList[groupPosition];
            var groupItem = childList.FirstOrDefault(x => x.Key.Name == groupName);
            return groupItem.Value.Count;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return headerList[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {

            var groupName = headerList[groupPosition];
            var groupItem = childList.FirstOrDefault(x => x.Key.Name == groupName);
            ImageView imageSource;
            CheckBox chekBox;

            if (convertView == null)
            {
                convertView = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.list_view_header, null);
                var txtView = (TextView)convertView.FindViewById(Resource.Id.txtHeaderText);
                imageSource = (ImageView)convertView.FindViewById(Resource.Id.imagearrow);
                chekBox = (CheckBox)convertView.FindViewById(Resource.Id.cb_group);
                txtView.Text = groupItem.Key.Name;
                chekBox.Checked = groupItem.Key.IsCheked;


                #region Cb_Group_Change
                chekBox.Click += (object sender, EventArgs e) =>
                {
                    var chB = (CheckBox)sender;
                    if (chB.Checked)
                    {
                        chB.Checked = false;
                        
                        //groupItem.Key.IsCheked = false;
                        //groupItem.Value.ForEach(x => x.IsCheked = false);
                    }
                    else
                    {
                        chB.Checked = false;
                        groupItem.Key.IsCheked = false;
                        groupItem.Value.ForEach(x => x.IsCheked = false);
                        this.NotifyDataSetChanged();
                    }

                };
                #endregion
            }
            else
            {
                imageSource = (ImageView)convertView.FindViewById(Resource.Id.imagearrow);
                chekBox = (CheckBox)convertView.FindViewById(Resource.Id.cb_group);

                chekBox.Checked = groupItem.Key.IsCheked;
            }

            if (isExpanded == true)
                imageSource.SetImageResource(Resource.Drawable.up_arrow_one);
            else
                imageSource.SetImageResource(Resource.Drawable.down_arrow_one);

            return convertView;
        }



        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            CheckBox chekBox;
            var groupName = headerList[groupPosition];
            var groupItem = childList.FirstOrDefault(x => x.Key.Name == groupName);
            var childItem = groupItem.Value[childPosition];

            if (convertView == null)
            {
                convertView = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.expand_list_chlid, null);
                var txtHeader = (TextView)convertView.FindViewById(Resource.Id.txtChildText);
                var mtxtorImage = (TextView)convertView.FindViewById(Resource.Id.textorimage);
                var mImageortxt = (ImageView)convertView.FindViewById(Resource.Id.imageortext);
                chekBox = (CheckBox)convertView.FindViewById(Resource.Id.cb_child);
                chekBox.Checked = childItem.IsCheked;

                #region Cb_Chil_Change
                chekBox.Click += (object sender, EventArgs e) =>
                {
                    var chB = (CheckBox)sender;
                    childItem.IsCheked = chB.Checked;

                    if (childList[groupItem.Key].Exists(x => x.IsCheked == true))
                        groupItem.Key.IsCheked = true;
                    else
                        groupItem.Key.IsCheked = false;
                    this.NotifyDataSetChanged();
                };
                #endregion

                txtHeader.Text = groupItem.Value[childPosition].Name;
                txtHeader.SetTextColor(Android.Graphics.Color.ParseColor("#ff000000"));

                //if (groupItem.Value[childPosition].Value == "Done")
                //{
                //    mtxtorImage.Visibility = ViewStates.Visible;
                //    txtHeader.SetTextColor(Android.Graphics.Color.ParseColor("#ff000000"));
                //    mImageortxt.Visibility = ViewStates.Gone;
                //}
                //else if (groupItem.Value[childPosition].Value == "Pending")
                //{
                //    mImageortxt.Visibility = ViewStates.Visible;
                //    txtHeader.SetTextColor(Android.Graphics.Color.ParseColor("#585858"));
                //    mtxtorImage.Visibility = ViewStates.Gone;
                //}
                //else if (groupItem.Value[childPosition].Value == null)
                //{
                //    mImageortxt.Visibility = ViewStates.Gone;
                //    txtHeader.SetTextColor(Android.Graphics.Color.ParseColor("#ff000000"));
                //    mtxtorImage.Visibility = ViewStates.Gone;
                //}
            }
            else
            {
                chekBox = (CheckBox)convertView.FindViewById(Resource.Id.cb_child);
                chekBox.Checked = childItem.IsCheked;
            }

            return convertView;
        }



        public override void OnGroupExpanded(int groupPosition)
        {
            base.OnGroupExpanded(groupPosition);
        }


        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}
