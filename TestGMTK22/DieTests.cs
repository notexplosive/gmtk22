using FluentAssertions;
using GMTK22;
using Xunit;

namespace TestGMTK22
{
    public class DieTests
    {
        [Fact]
        public void die_with_empty_slots()
        {
            var die = new Die();

            Slot.ValidSlots.Should().HaveCount(9);

            foreach (var slot in Slot.ValidSlots)
            {
                die.At(slot).Should().Be(FillState.Empty);
            }
        }

        [Fact]
        public void die_with_one_filled_slot()
        {
            var die = new Die();

            die.Fill(Slot.TopRight);

            die.At(Slot.TopRight).Should().Be(FillState.Filled);
        }
        
        [Fact]
        public void fill_and_clear_slot()
        {
            var die = new Die();

            die.Fill(Slot.TopRight);
            die.Clear(Slot.TopRight);

            die.At(Slot.TopRight).Should().Be(FillState.Empty);
        }
        
        [Fact]
        public void fill_twice_and_clear_slot()
        {
            var die = new Die();

            die.Fill(Slot.TopRight);
            die.Fill(Slot.TopRight);
            die.Clear(Slot.TopRight);

            die.At(Slot.TopRight).Should().Be(FillState.Empty);
        }
    }
}