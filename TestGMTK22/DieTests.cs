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

            Slot.All.Should().HaveCount(9);

            foreach (var slot in Slot.All)
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
        
        [Fact]
        public void can_clone_dice()
        {
            var original = new Die();

            original.Fill(Slot.TopRight);
            original.Fill(Slot.BottomLeft);
            
            var clone = original.Clone();
            
            clone.Fill(Slot.CenterCenter);
            original.Clear(Slot.TopRight);

            original.At(Slot.CenterCenter).Should().Be(FillState.Empty);
            clone.At(Slot.CenterCenter).Should().Be(FillState.Filled);
            original.At(Slot.TopRight).Should().Be(FillState.Empty);
            clone.At(Slot.TopRight).Should().Be(FillState.Filled);
            original.At(Slot.BottomLeft).Should().Be(FillState.Filled);
            clone.At(Slot.BottomLeft).Should().Be(FillState.Filled);
        }

        [Fact]
        public void move_pips_up()
        {
            var die = new Die();
            die.Fill(Slot.TopRight);
            die.Fill(Slot.BottomCenter);
            die.Fill(Slot.CenterLeft);
            die.Fill(Slot.BottomLeft);

            var result = die.Transform(new MoveDieTransform(Direction.Up));

            result.At(Slot.TopRight).Should().Be(FillState.Filled);
            result.At(Slot.BottomCenter).Should().Be(FillState.Empty);
            result.At(Slot.CenterCenter).Should().Be(FillState.Filled);
            result.At(Slot.CenterLeft).Should().Be(FillState.Filled);
            result.At(Slot.TopLeft).Should().Be(FillState.Filled);
            result.At(Slot.BottomLeft).Should().Be(FillState.Empty);
        }
        
        [Fact]
        public void move_pips_right()
        {
            var die = new Die();
            die.Fill(Slot.TopRight);
            die.Fill(Slot.BottomCenter);
            die.Fill(Slot.CenterLeft);
            die.Fill(Slot.BottomLeft);

            var result = die.Transform(new MoveDieTransform(Direction.Right));

            result.At(Slot.TopRight).Should().Be(FillState.Filled);
            result.At(Slot.BottomCenter).Should().Be(FillState.Filled);
            result.At(Slot.CenterLeft).Should().Be(FillState.Empty);
            result.At(Slot.BottomLeft).Should().Be(FillState.Empty);
            
            result.At(Slot.CenterCenter).Should().Be(FillState.Filled);
            result.At(Slot.BottomRight).Should().Be(FillState.Filled);
        }
        
        [Fact]
        public void move_pips_down()
        {
            var die = new Die();
            die.Fill(Slot.TopRight);
            die.Fill(Slot.BottomCenter);
            die.Fill(Slot.CenterLeft);
            die.Fill(Slot.BottomLeft);

            var result = die.Transform(new MoveDieTransform(Direction.Down));

            result.At(Slot.TopRight).Should().Be(FillState.Empty);
            result.At(Slot.BottomCenter).Should().Be(FillState.Filled);
            result.At(Slot.CenterLeft).Should().Be(FillState.Filled);
            result.At(Slot.BottomLeft).Should().Be(FillState.Filled);
            
            result.At(Slot.CenterRight).Should().Be(FillState.Filled);
        }
    }
}