using System.Collections.Generic;
using NUnit.Framework;
using ShipsInSpace.Models;

namespace UnitTests.AttributesTests
{
    public class TotalWeightNotExceeded
    {
        public ShipViewModel Ship;
        public ShipsInSpace.Models.CustomAttributes.TotalWeightNotExceeded MyTotalWeightNotExceeded;

        [SetUp]
        public void Setup()
        {
            Ship = new ShipViewModel
            {
                Id = 1,
                Name = "DeadlyFighterOfDeadliness",
                Wings = new List<WingViewModel>()
                {
                    new WingViewModel()
                    {
                        Id = 4,
                        Name = "O-Fence",
                        Agility = 0,
                        Speed = 0,
                        Energy = 15,
                        NumberOfHardpoints = 2,
                        Weight = 250,
                        Hardpoint = new List<WeaponViewModel>()
                        {
                            new WeaponViewModel() {
                                Id = 2,
                                Name= "Crusher",
                                Weight=89,
                                DamageType= DamageTypeEnum.Gravity,
                                EnergyDrain = 56
                            },
                            new WeaponViewModel() {
                                Id = 2,
                                Name= "Crusher",
                                Weight=89,
                                DamageType= DamageTypeEnum.Gravity,
                                EnergyDrain = 56
                            },
                        }
                    },
                    new WingViewModel()
                    {
                        Id = 4,
                        Name = "O-Fence",
                        Agility = 0,
                        Speed = 0,
                        Energy = 15,
                        NumberOfHardpoints = 2,
                        Weight = 250,
                        Hardpoint = new List<WeaponViewModel>()
                        {
                            new WeaponViewModel() {
                                Id = 2,
                                Name= "Crusher",
                                Weight=89,
                                DamageType= DamageTypeEnum.Gravity,
                                EnergyDrain = 56
                            },
                            new WeaponViewModel() {
                                Id = 2,
                                Name= "Crusher",
                                Weight=89,
                                DamageType= DamageTypeEnum.Gravity,
                                EnergyDrain = 56
                            },
                        }
                    },
                },
                NumberOfWings = 2,
                Engine = new EngineViewModel()
                {
                    Id = 3,
                    Name = "Constellation Class",
                    Energy = 200,
                    Weight = 200
                },
                Hull = new HullViewModel
                {
                    Id = 2,
                    Name = "Neptunus",
                    Agility = 100,
                    Speed = 100,
                    ColdShielding = 50,
                    HeatShielding = 0,
                    DefaultMaximumTakeOffMass = TakeOffMassEnum.Tank,
                    ActualTakeOffMass = (int)TakeOffMassEnum.Tank,
                },
            };

            MyTotalWeightNotExceeded = new ShipsInSpace.Models.CustomAttributes.TotalWeightNotExceeded();
        }

        [Test]
        public void ShipShouldHaveNoConflict()
        {
            Assert.IsTrue(MyTotalWeightNotExceeded.IsValid(Ship));
        }

        [Test]
        public void ConfigurationWeightsTooMuch()
        {
            Ship.Hull = new HullViewModel()
            {
                Id = 4,
                Name = "RaceWing",
                Agility = 205,
                Speed = 150,
                ColdShielding = 25,
                HeatShielding = 25,
                DefaultMaximumTakeOffMass = TakeOffMassEnum.Interceptor,
                ActualTakeOffMass = (int)TakeOffMassEnum.Interceptor,
            };

            //The isvalid method should return false since this should not be possible and therefore we invert the bool
            Assert.IsTrue(!MyTotalWeightNotExceeded.IsValid(Ship));
        }
    }
}
