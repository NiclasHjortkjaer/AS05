using System.Collections.Generic;
using Xunit;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        Program test = new Program();

        public TestAssemblyTests(){
            test.Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 30},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new CheeseItem {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new LegendaryItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new TicketItem {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 5},
                    new ConjuredItem {Name = "Conjured Mana Cake", SellIn = 11, Quality = 8}
                };

            test.UpdateQuality();
        }

        [Fact]
        public void Sulfurus_quality_doesnt_decrement()
        {
            var expected = 80;
            var actual = test.Items[3].Quality;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Sulfurus_sellin_doesnt_decrement()
        {
            var expected = 0;
            var actual = test.Items[3].SellIn;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Brie_quality_increments_one_when_not_expired()
        {
            var expected = 6;
            var actual = test.Items[1].Quality;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Brie_quality_increments_two_when_expired()
        {
            var item = test.Items[2];

            item.SellIn = -1;

            var expected = item.Quality +2;

            test.UpdateQuality();

            var actual = item.Quality;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Backstage_quality_increments_when_SellIn_higher_than_10()
        {
            var expected = 6;
            var actual = test.Items[1].Quality;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Backstage_quality_increments_when_SellIn_lower_than_10_over_5()
        {
            var item = test.Items[4];
          
            item.SellIn = 8;

            var oldQuality = item.Quality;
            
            test.UpdateQuality();
            var newQuality = oldQuality+2;
            
            Assert.Equal(newQuality, item.Quality);
        }

        [Fact]
        public void Backstage_quality_increments_when_SellIn_lower_than_5_over_0()
        {
            var item = test.Items[4];
          
            item.SellIn = 3;

            var oldQuality = item.Quality;

            test.UpdateQuality();
            var newQuality = oldQuality+3;

            Assert.Equal(newQuality, item.Quality);
        }

        [Fact]
        public void Backstage_quality_is0_when_SellIn_is_over_date()
        {
            var item = test.Items[4];
          
            item.SellIn = -1;

            test.UpdateQuality();
            
            var expected = 0;
            var actual = item.Quality;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Item_quality_decrement_one_when_SellIn_not_negative()
        {
            //new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 30}
            var item = test.Items[0];

            var expected = item.SellIn-1;

            test.UpdateQuality();

            var actual = item.SellIn;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Item_quality_decrement_two_when_SellIn_negative()
        {
            var item = test.Items[0];

            item.SellIn = -1;

            var expected = item.Quality -2;

            test.UpdateQuality();

            var actual = item.Quality;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Quality_is_never_above_50()
        {
            var item = test.Items[2];

            item.Quality = 50;

            var expected = 50;

            test.UpdateQuality();

            var actual = item.Quality;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Quality_is_never_below_0()
        {
            var item = test.Items[0];

            item.Quality = 0;

            var expected = 0;

            test.UpdateQuality();

            var actual = item.Quality;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Conjured_quality_decrements_with_2()
        {
            var item = test.Items[5];

            var expected = item.Quality - 2;

            test.UpdateQuality();

            var actual = item.Quality;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Conjured_quality_decrements_with_4_when_expired()
        {
            var item = test.Items[5];
            item.SellIn = -2;
            var expected = item.Quality - 4;

            test.UpdateQuality();

            var actual = item.Quality;

            Assert.Equal(expected, actual);
        }
    }
}