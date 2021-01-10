using System.Collections.Generic;
using NUnit.Framework;
using ShipsInSpace.Models;

namespace UnitTests.AttributesTests
{
    public class CheckCombinations
    {
        public ShipViewModel Ship;
        public ShipsInSpace.Models.CustomAttributes.CheckCombinations MyCheckCombinations;

        [SetUp]
        public void Setup()
        {
            //create a basic ship with no conflict in combinations
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

            MyCheckCombinations = new ShipsInSpace.Models.CustomAttributes.CheckCombinations();
        }

        [Test]
        public void ShipShouldHaveNoConflict()
        {
            Assert.IsTrue(MyCheckCombinations.IsValid(Ship));
        }

        [Test]
        public void ShipCantUseImploderWeaponWithIntrepidClassEngine()
        {
            Ship.Engine = new EngineViewModel()
            {
                Id = 2,
                Name = "Intrepid Class",
                Energy = 350,
                Weight = 350,
            };

            Ship.Wings[0].Hardpoint[0] = new WeaponViewModel()
            {
                Id = 9,
                Name = "Imploder",
                Weight = 270,
                DamageType = DamageTypeEnum.Gravity,
                EnergyDrain = 43
            };

            //The isvalid method should return false since this should not be possible and therefore we invert the bool
            Assert.IsTrue(!MyCheckCombinations.IsValid(Ship));
        }

        [Test]
        public void ShipCantUseColdWeaponWithHeatWeapon()
        {
            Ship.Wings[0].Hardpoint[0] = new WeaponViewModel()
            {
                Id = 1,
                Name = "Fury Cannon",
                Weight = 76,
                DamageType = DamageTypeEnum.Heat,
                EnergyDrain = 52
            };
            Ship.Wings[0].Hardpoint[1] = new WeaponViewModel()
            {
                Id = 4,
                Name = "Freeze Ray",
                Weight = 24,
                DamageType = DamageTypeEnum.Cold,
                EnergyDrain = 52
            };

            //The isvalid method should return false since this should not be possible and therefore we invert the bool
            Assert.IsTrue(!MyCheckCombinations.IsValid(Ship));
        }

        [Test]
        public void ShipCantUseStasisWeaponWithGravityWeapon()
        {
            Ship.Wings[0].Hardpoint[0] = new WeaponViewModel()
            {
                Id = 2,
                Name = "Crusher",
                Weight = 89,
                DamageType = DamageTypeEnum.Gravity,
                EnergyDrain = 56
            };
            Ship.Wings[0].Hardpoint[1] = new WeaponViewModel()
            {
                Id = 10,
                Name = "Levitator",
                Weight = 59,
                EnergyDrain = 56,
                DamageType = DamageTypeEnum.Statis
            };

            //The isvalid method should return false since this should not be possible and therefore we invert the bool
            Assert.IsTrue(!MyCheckCombinations.IsValid(Ship));
        }

        [Test]
        public void NullifierWeaponCantBeEquippedAlloneOnWing()
        {
            Ship.Wings[0].Hardpoint = new List<WeaponViewModel>()
            {
                new WeaponViewModel()
                {
                    Id = 14,
                    Name= "Nullifier",
                    Weight= 23,
                    DamageType= DamageTypeEnum.Gravity,
                    EnergyDrain = 43
                }
        };

            //The isvalid method should return false since this should not be possible and therefore we invert the bool
            Assert.IsTrue(!MyCheckCombinations.IsValid(Ship));
        }
    }
}