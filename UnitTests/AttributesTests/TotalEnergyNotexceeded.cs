using System.Collections.Generic;
using NUnit.Framework;
using ShipsInSpace.Models;

namespace UnitTests.AttributesTests
{
    public class TotalEnergyNotExceeded
    {
        public ShipViewModel Ship;
        public ShipsInSpace.Models.CustomAttributes.TotalEnergyNotExceeded MyTotalEnergyNotExceeded;

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
                },
            };

            MyTotalEnergyNotExceeded = new ShipsInSpace.Models.CustomAttributes.TotalEnergyNotExceeded();
        }

        [Test]
        public void ShipShouldHaveNoConflict()
        {
            Assert.IsTrue(MyTotalEnergyNotExceeded.IsValid(Ship));
        }

        [Test]
        public void WeaponsUseTooMuchEnergy()
        {
            Ship.Engine = new EngineViewModel()
            {
                Id = 1,
                Name = "Galaxy Class",
                Energy = 150,
                Weight = 150
            };

            Ship.Wings[0].Hardpoint[0] = new WeaponViewModel()
            {
                Id = 3,
                Name = "Flamethrower",
                Weight = 30,
                DamageType = DamageTypeEnum.Heat,
                EnergyDrain = 74
            };

            //The isvalid method should return false since this should not be possible and therefore we invert the bool
            Assert.IsTrue(!MyTotalEnergyNotExceeded.IsValid(Ship));
        }
    }
}